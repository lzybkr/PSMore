using PSMore.Formatting;
using System.DirectoryServices;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(DirectoryEntry))]
    internal abstract class DirectoryEntryFormatProxy
    {
        [DisplayProperty(Position = 0)] public object distinguishedName;
        [DisplayProperty(Position = 1)] public object Path;
    }

}

