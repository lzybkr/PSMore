using PSMore.Formatting;
using System.IO;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(DirectoryInfo))]
    internal abstract class DirectoryInfoFormatProxy
    {
        [DisplayProperty(Default = true)] public object Name;
    }

    [FormatProxy(typeof(FileInfo))]
    internal abstract class FileInfoFormatProxy
    {
        [DisplayProperty(Position = 0)] public object LastWriteTime;
        [DisplayProperty(Position = 1)] public object Length;
        [DisplayProperty(Position = 2)] public object Name;
    }

}

