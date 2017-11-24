
using PSMore.FormatAttributes;
using System.Diagnostics;

#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{
    [FormatProxy(typeof(Process))]
    internal abstract class ProcessFormatProxy
    {
        [DefaultDisplayProperty(4)]
        public string Name;

        [DefaultDisplayProperty(0)]
        public int Id;

        [DefaultDisplayProperty(1)]
        public int Handles;

        [DefaultDisplayProperty(2)]
        public double CPU;

        [DefaultDisplayProperty(3)]
        public int SI;
    }
}
