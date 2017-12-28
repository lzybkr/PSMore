using System.Management.Automation;
using PSMore.Formatting;

namespace PSMore.Commands
{
    /// <summary>
    /// Implementation of the <c>Format-List</c> command.
    /// </summary>
    [Cmdlet("Format", "PSMoreList")]
    [Alias("psmorelist")]
    public class FormatListCommand : BaseFormatCommand
    {
        /// <summary>
        /// Initialize the descriptor for formatting objects.
        /// </summary>
        protected override void BeginProcessing()
        {
            _descriptor = Property != null ? new ListDescriptor(Property) : new ListDescriptor();
        }
    }
}
