using System;
using System.Collections.Generic;
using System.Management.Automation;
using PSMore.Formatting;

namespace PSMore
{
    [Cmdlet("Out", "PSMoreString")]
    [Alias("psmoreos")]
    public class OutStringCommand : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public object[] InputObject { get; set; }

        [Parameter]
        public SwitchParameter Stream { get; set; }

        private List<string> _lines;

        protected override void BeginProcessing()
        {
            if (!Stream) _lines = new List<string>();
        }

        private bool notFirst = false;
        protected override void ProcessRecord()
        {
            if (InputObject == null || InputObject.Length == 0) return;

            foreach (var obj in InputObject)
            {
                if (notFirst)
                {
                    if (Stream) { WriteObject(""); }
                    else { _lines.Add(""); }
                }
                notFirst = true;

                foreach (var line in FormatEngine.Format(obj))
                {
                    if (Stream) { WriteObject(line); }
                    else { _lines.Add(line); }
                }
            }
        }

        protected override void EndProcessing()
        {
            if (!Stream)
            {
                WriteObject(string.Join(Environment.NewLine, _lines));
            }
        }
    }
}
