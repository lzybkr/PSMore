using System;
using System.Collections.Generic;
using System.Management.Automation;
using PSMore.Formatting;

namespace PSMore
{
    [Cmdlet("Out", "PSMore")]
    public class PSMore : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public object[] InputObject { get; set; }

        private List<object> _objects;

        protected override void BeginProcessing()
        {
            _objects = new List<object>();
        }

        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            _objects.AddRange(InputObject);
            foreach (var obj in InputObject)
            {
                foreach (var line in FormatEngine.Format(obj))
                    Console.WriteLine(line);
            }
        }
    }
}
