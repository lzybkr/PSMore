using PSMore.FormatAttributes;
using System.Diagnostics;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(Process))]
    internal abstract class ProcessFormatProxy
    {
        [DisplayProperty(Position = 0)] public object Id;
        [DisplayProperty(Position = 1)] public object Handles;
        [DisplayProperty(Position = 2)] public object CPU;
        [DisplayProperty(Position = 3)] public object SI;
        [DisplayProperty(Position = 4)] public object Name;
    }

}

