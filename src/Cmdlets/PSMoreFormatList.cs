using System.Management.Automation;
using System.Management.Automation.Internal;
using PSMore.Formatting;

namespace PSMore
{
    [Cmdlet("Format", "PSMoreList")]
    [Alias("psmorelist")]
    public class FormatListCommand : PSCmdlet
    {
        [Parameter(Position = 0)]
        public object[] Property { get; set; }

        [Parameter(ValueFromPipeline = true)]
        public PSObject InputObject { get; set; }

        private ListFormat _formatDirective;

        protected override void BeginProcessing()
        {
            if (Property != null)
            {
                _formatDirective = new ListFormat(Property);
            }
            else
            {
                _formatDirective = new ListFormat();
            }
        }

        protected override void ProcessRecord()
        {
            if (InputObject == null || ReferenceEquals(InputObject, AutomationNull.Value)) return;

            void AddDirectiveAndWrite(object obj)
            {
                var psobj = obj as PSObject ?? new PSObject(obj);
                psobj.Properties.Add(new PSNoteProperty(FormatEngine.AttachedFormatPropertyName , _formatDirective));
                WriteObject(obj);
            }

            var enumerable = LanguagePrimitives.GetEnumerable(InputObject);
            if (enumerable != null)
            {
                foreach (var o in enumerable)
                {
                    AddDirectiveAndWrite(o);
                }
            }
            else
            {
                AddDirectiveAndWrite(InputObject);
            }
        }
    }
}
