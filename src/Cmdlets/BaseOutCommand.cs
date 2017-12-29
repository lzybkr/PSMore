using System;
using System.Management.Automation;
using System.Threading.Tasks.Dataflow;
using PSMore.Formatting;

namespace PSMore
{
    /// <summary>
    /// </summary>
    public abstract class BaseOutCommand : PSCmdlet
    {
        /// <summary>
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public object[] InputObject { get; set; }

        /// <summary>
        /// </summary>
        internal /*private protected*/ FormattingPipeline FormattingPipeline { get; private set; }
        ITargetBlock<string> _lineOutputBlock;

        /// <summary>
        /// </summary>
        protected abstract ITargetBlock<string> GetLineOutputAction();

        /// <summary>
        /// </summary>
        protected override void BeginProcessing()
        {
            _lineOutputBlock = GetLineOutputAction();
            FormattingPipeline = new FormattingPipeline(_lineOutputBlock);
        }

        /// <summary>
        /// </summary>
        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            foreach (var obj in InputObject)
            {
                FormattingPipeline.Process(obj);
            }
        }

        /// <summary>
        /// </summary>
        protected override void EndProcessing()
        {
            FormattingPipeline.Complete();
            _lineOutputBlock.Complete();
            _lineOutputBlock.Completion.Wait();
        }

        /// <summary>
        /// </summary>
        protected override void StopProcessing()
        {
            _lineOutputBlock.Complete();
            _lineOutputBlock.Completion.Wait();
            FormattingPipeline.Cancel();
        }
    }
}
