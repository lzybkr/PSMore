using PSMore.Formatting;
using System.ServiceProcess;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(ServiceController))]
    internal abstract class ServiceControllerFormatProxy
    {
        [DisplayProperty(Position = 0)] public object Status;
        [DisplayProperty(Position = 1)] public object Name;
        [DisplayProperty(Position = 2)] public object DisplayName;
    }

}

