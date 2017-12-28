using System.Management.Automation;
using System.Management.Automation.Internal;
using PSMore.Formatting;

namespace PSMore
{
    /// <summary>
    /// The base of all our formatting commands which all just add a note property
    /// to each object specifying how to format the object.
    /// </summary>
    public abstract class BaseFormatCommand : PSCmdlet
    {
        /// <summary>
        /// The parameter that specifies the properties (named or calculated)
        /// to be used when formatting the object.
        /// </summary>
        [Parameter(Position = 0)]
        public object[] Property { get; set; }

        /// <summary>
        /// The input object to format.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public PSObject InputObject { get; set; }

        /// <summary>
        /// </summary>
        protected Descriptor _descriptor;

        /// <summary>
        /// No formatting is performed here, that is deferred until output.
        /// Instead, we just add a <see cref="PSNoteProperty"/> to each object
        /// that specifies the format which will then be used during output.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (InputObject == null || ReferenceEquals(InputObject, AutomationNull.Value)) return;

            void AddDirectiveAndWrite(object obj)
            {
                var psobj = obj as PSObject ?? new PSObject(obj);
                psobj.Properties.Add(new PSNoteProperty(FormatEngine.AttachedFormatPropertyName, _descriptor));
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

