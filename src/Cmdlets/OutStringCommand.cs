using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks.Dataflow;
using PSMore.Formatting;

namespace PSMore.Commands
{
    /// <summary>
    /// </summary>
    [Cmdlet("Out", "PSMoreString")]
    [Alias("psmoreos")]
    public class OutStringCommand : BaseOutCommand
    {
        /// <summary>
        /// </summary>
        [Parameter]
        public SwitchParameter Stream { get; set; }

        private StringBuilder _sb;
        private BufferBlock<string> _buffer;
        private Action<string> _actionOnEachLine;

        /// <summary>
        /// </summary>
        protected override ITargetBlock<string> GetLineOutputAction()
        {
            return _buffer ?? (_buffer = new BufferBlock<string>());
        }

        private void ProcessBuffer()
        {
            while (_buffer.Count > 0)
            {
                _actionOnEachLine(_buffer.Receive());
            }
        }

        /// <summary>
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (!Stream)
            {
                _sb = new StringBuilder();
                _actionOnEachLine = line => _sb.AppendLine(line);
            }
            else
            {
                _actionOnEachLine = WriteObject;
            }
        }

        /// <summary>
        /// </summary>
        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            foreach (var obj in InputObject)
            {
                FormattingPipeline.Process(obj);
                ProcessBuffer();
            }
        }

        /// <summary>
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
            ProcessBuffer();
            if (!Stream) WriteObject(_sb.ToString());
        }
    }
}
