
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PSMore.Formatting
{
    /// <summary>
    /// The general shape of a format, used to guide selection of a formatting descriptor.
    /// </summary>
    internal enum Style
    {
        Default,
        List,
        Table
    }

    /// <summary>
    /// The criteria used to select or reuse a formatting descriptor.
    /// </summary>
    internal class SelectionCriteria
    {
        /// <summary>
        /// Create an instance of the class <see cref="SelectionCriteria"/>.
        /// </summary>
        public SelectionCriteria(Type type, Style style, Descriptor descriptor, string name)
        {
            Type       = type;
            Style      = style;
            Descriptor = descriptor;
            Name       = name;
        }

        /// <summary>
        /// The type of object to be formatted.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// The name of descriptor (specified with the <c>-View</c> parameter to various <c>Format-*</c> cmdlets.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Specify a general format without a descriptor.
        /// </summary>
        public Style Style { get; }

        /// <summary>
        /// Specify a descriptor. The descriptor does not need to be specific to any type, the engine
        /// will specialize it for each different type.
        /// </summary>
        public Descriptor Descriptor { get; }

        internal static Expression GetCompatibleCall(Expression criteria, Descriptor descriptor, Expression toFormat)
        {
            return Expression.Call(criteria, CompatibleMethodInfo, Expression.Constant(descriptor), toFormat);
        }

        private static readonly MethodInfo CompatibleMethodInfo =
            typeof(SelectionCriteria).GetMethod(nameof(CompatibleWithDescriptor), BindingFlags.NonPublic | BindingFlags.Instance);

        private bool CompatibleWithDescriptor(Descriptor descriptor, object obj)
        {
            if (descriptor.Type != Type) return false;
            if (descriptor.When != null && !descriptor.When.Applies(obj)) return false;

            switch (Style)
            {
                case Style.List:
                    if (!(descriptor is ListDescriptor)) return false;
                    break;

                case Style.Table:
                    if (!(descriptor is TableDescriptor)) return false;
                    break;
            }

            return Descriptor == null || descriptor.Equals(Descriptor);
        }
    }

    abstract class FormatInstruction
    {
    }

    class EmitLine : FormatInstruction
    {
        public string Line { get; set; }
    }

    class EmitPropertyLine : EmitLine { }

    class EmitTableRow : FormatInstruction
    {
        public TableDescriptor Descriptor { get; set; }
        public string[] Columns { get; set; }
    }

    /// <summary>
    /// A static class with the primary formatting methods for arbitrary objects.
    /// </summary>
    public static class FormatEngine
    {
        private static readonly CallSite<Func<CallSite, object, SelectionCriteria, IEnumerable<FormatInstruction>>> _site =
            CallSite<Func<CallSite, object, SelectionCriteria, IEnumerable<FormatInstruction>>>.Create(FormatBinder.Instance);

        /// <summary>
        /// Specifies the name of the property attached to objects when formatting is specified.
        /// This allows the use of format cmdlets in the pipeline.
        /// </summary>
        public const string AttachedFormatPropertyName = "__PSMoreFormat";

        /// <summary>
        /// Format an object using the default format for that object.
        /// </summary>
        /// <param name="obj">The object to format.</param>
        internal static IEnumerable<FormatInstruction> Format(object obj)
        {
            return Format(obj, descriptor: null);
        }

        /// <summary>
        /// Format an object using the specified descriptor.
        /// </summary>
        /// <param name="obj">The object to format.</param>
        /// <param name="descriptor">The formatting descriptor.</param>
        internal static IEnumerable<FormatInstruction> Format(object obj, Descriptor descriptor)
        {
            Type type;
            if (obj is PSObject psobj)
            {
                type = psobj.BaseObject.GetType();
            }
            else
            {
                type = obj.GetType();
                psobj = new PSObject(obj);
            }

            if (descriptor == null)
            {
                descriptor = psobj.Properties[AttachedFormatPropertyName]?.Value as Descriptor;
            }

            Style style = Style.Default;
            if (descriptor != null)
            {
                switch (descriptor)
                {
                    case ListDescriptor lf:
                        style = Style.List;
                        break;

                    case TableDescriptor tf:
                        style = Style.Table;
                        break;
                }

                if (descriptor.Type == null)
                {
                    descriptor = descriptor.Clone(type);
                }
            }

            var criteria = new SelectionCriteria(
                descriptor: descriptor,
                style: style,
                type: type,
                name: null);
            return _site.Target.Invoke(_site, psobj, criteria);
        }
    }

    class FormattingPipeline
    {
        internal FormattingPipeline(ITargetBlock<string> lineOutputBlock)
        {
            _lineOutputBlock = lineOutputBlock;
        }

        private readonly ITargetBlock<string> _lineOutputBlock;
        bool _lastWasTableRow;
        bool _initialBlankLineWasOutput;
        TableDescriptor _currentTableDescriptor;
        Task _tableRowProcessor;
        CancellationTokenSource _tableProcessingTokenSource;
        BufferBlock<EmitTableRow> _tableRowsBufferBlock;

        internal void Process(object obj)
        {
            bool sawList = false;
            foreach (var instr in FormatEngine.Format(obj))
            {
                switch (instr)
                {
                    case EmitPropertyLine epl:
                        if (!_initialBlankLineWasOutput)
                        {
                            _initialBlankLineWasOutput = true;
                            _lineOutputBlock.Post("");
                        }
                        _lineOutputBlock.Post(epl.Line);
                        _lastWasTableRow = false;
                        sawList = true;
                        break;

                    case EmitLine el:
                        // We don't emit an initial blank line for basic formatting
                        _lineOutputBlock.Post(el.Line);
                        _lastWasTableRow = false;
                        break;

                    case EmitTableRow etr:
                        if (!_initialBlankLineWasOutput)
                        {
                            _initialBlankLineWasOutput = true;
                            _lineOutputBlock.Post("");
                        }

                        if (!object.ReferenceEquals(_currentTableDescriptor, etr.Descriptor))
                        {
                            // Cancel any previous processing.
                            if (_tableRowProcessor != null)
                            {
                                _tableRowsBufferBlock.Complete();
                                _tableRowProcessor.Wait();
                            }
                            _tableProcessingTokenSource = new CancellationTokenSource();
                            var token = _tableProcessingTokenSource.Token;
                            _tableRowsBufferBlock = new BufferBlock<EmitTableRow>(
                                new DataflowBlockOptions { CancellationToken = token });
                            _tableRowProcessor = ProcessTableRows(_tableRowsBufferBlock, token);
                            _currentTableDescriptor = etr.Descriptor;
                        }

                        _tableRowsBufferBlock.Post(etr);
                        _lastWasTableRow = true;
                        break;
                }
            }
            if (sawList)
            {
                _lineOutputBlock.Post("");
            }
        }

        async Task ProcessTableRows(BufferBlock<EmitTableRow> bufferedRows, CancellationToken token)
        {
            await Task.Delay(100, token).ConfigureAwait(true);

            var sb = new StringBuilder();
            int cols = _currentTableDescriptor.Columns.Count;
            var colWidths = new int[cols];

            if (bufferedRows.TryReceiveAll(out var rows))
            {
                foreach (var row in rows)
                {
                    for (int i = 0; i < cols; i++)
                    {
                        colWidths[i] = Math.Max(colWidths[i], row.Columns[i]?.Length ?? 0);
                    }
                }

                foreach (var line in _currentTableDescriptor.FormatHeader(sb, colWidths))
                {
                    _lineOutputBlock.Post(line);
                }

                foreach (var row in rows)
                {
                    _currentTableDescriptor.FormatRow(sb, colWidths, row.Columns);
                    _lineOutputBlock.Post(sb.ToString());
                }
            }

            while (await bufferedRows.OutputAvailableAsync(token))
            {
                var row = bufferedRows.Receive();
                _currentTableDescriptor.FormatRow(sb, colWidths, row.Columns);
                _lineOutputBlock.Post(sb.ToString());
            }
        }

        internal void Complete()
        {
            _tableRowsBufferBlock?.Complete();
            _tableRowProcessor?.Wait();

            if (_lastWasTableRow)
            {
                _lineOutputBlock.Post("");
            }
        }

        internal void Cancel()
        {
            _tableProcessingTokenSource?.Cancel();
        }
    }
}
