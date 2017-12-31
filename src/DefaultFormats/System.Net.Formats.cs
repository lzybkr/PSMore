using PSMore.FormattingAttributes;
using System.Net;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(IPAddress))]
    internal abstract class IPAddressFormatProxy
    {
        [DisplayProperty(Default = true)] public object IPAddressToString;
    }

}

