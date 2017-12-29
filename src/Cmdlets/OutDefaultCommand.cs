using System;
using System.Management.Automation;
using System.Threading.Tasks.Dataflow;

namespace PSMore.Commands
{
    /// <summary>
    /// </summary>
    [Cmdlet("Out", "PSMoreDefault")]
    public class OutDefaultCommand : BaseOutCommand
    {
        /// <summary>
        /// </summary>
        protected override ITargetBlock<string> GetLineOutputAction()
        {
            return new ActionBlock<string>((Action<string>)Console.WriteLine);
        }
    }
}
