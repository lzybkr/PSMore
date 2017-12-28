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
        ITargetBlock<string> _lineOutputAction;

        /// <summary>
        /// </summary>
        protected abstract ITargetBlock<string> GetLineOutputAction();

        /// <summary>
        /// </summary>
        protected override void BeginProcessing()
        {
            _lineOutputAction = GetLineOutputAction();
            FormattingPipeline = new FormattingPipeline(_lineOutputAction);
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

        void CompleteNestedPipeline()
        {
            _lineOutputAction.Complete();
            async void Wait()
            {
                await _lineOutputAction.Completion;
            }
            Wait();
        }

        /// <summary>
        /// </summary>
        protected override void EndProcessing()
        {
            CompleteNestedPipeline();
            FormattingPipeline.Complete();
        }

        /// <summary>
        /// </summary>
        protected override void StopProcessing()
        {
            CompleteNestedPipeline();
            FormattingPipeline.Cancel();
        }
    }
}
