using System.Management.Automation;
using PSMore.Formatting;

namespace PSMore.Commands
{
    /// <summary>
    /// </summary>
    [Cmdlet("Format", "PSMoreTable")]
    [Alias("psmoretable")]
    public class FormatTableCommand : BaseFormatCommand
    {
        /// <summary>
        /// </summary>
        protected override void BeginProcessing()
        {
            _descriptor = Property != null ? new TableDescriptor(Property) : new TableDescriptor();
        }
    }
}

