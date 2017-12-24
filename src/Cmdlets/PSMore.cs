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
        private BufferBlock<string> _buffer;
        private CancellationTokenSource _cancellationTokenSource;

        protected override void BeginProcessing()
        {
            _objects = new List<object>();
            _buffer = new BufferBlock<string>();
            _cancellationTokenSource = new CancellationTokenSource();
            _outputTask = OutputItems(_buffer);
        }

        async Task OutputItems(ISourceBlock<string> source)
        {
            while (await source.OutputAvailableAsync(_cancellationTokenSource.Token))
            {
                Console.WriteLine(source.Receive());
            }
        }

        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            _objects.AddRange(InputObject);
            foreach (var obj in InputObject)
            {
                if (_objects.Count > 1) _buffer.Post("");
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
