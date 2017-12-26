using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PSMore.Formatting;

namespace PSMore
{
    [Cmdlet("Out", "PSMore")]
    public class PSMore : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public object[] InputObject { get; set; }

        private List<object> _objects;
        private Task _outputTask;
        private BufferBlock<FormatInstruction> _buffer;
        private CancellationTokenSource _cancellationTokenSource;

        static EmitLine BlankLine = new EmitLine { Line = "" };
        protected override void BeginProcessing()
        {
            _objects = new List<object>();
            _buffer = new BufferBlock<FormatInstruction>();
            _cancellationTokenSource = new CancellationTokenSource();
            _outputTask = OutputItems(_buffer);
        }

        async Task OutputItems(ISourceBlock<FormatInstruction> source)
        {
            while (await source.OutputAvailableAsync(_cancellationTokenSource.Token))
            {
                var instr = source.Receive();
                switch (instr)
                {
                    case EmitLine el:
                        Console.WriteLine(el.Line);
                        break;
                }
            }
        }

        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            _objects.AddRange(InputObject);
            foreach (var obj in InputObject)
            {
                if (_objects.Count > 1) _buffer.Post(BlankLine);
                foreach (var line in FormatEngine.Format(obj))
                {
                    _buffer.Post(line);
                }
            }
        }

        protected override void EndProcessing()
        {
            _buffer.Complete();
            _outputTask.Wait();
        }

        protected override void StopProcessing()
        {
            _cancellationTokenSource.Cancel();
            _buffer.Complete();
            _outputTask.Wait();
        }
    }
}
