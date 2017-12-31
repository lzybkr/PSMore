using PSMore.FormattingAttributes;
using System.Diagnostics;
using PSMore.Formatting;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(Process))]
    internal abstract class ProcessDefaultFormatProxy
    {
        [DisplayProperty(Position = 0)] public object Id;
        [DisplayProperty(Position = 1)] public object Handles;
        [DisplayProperty(Position = 2)] public object CPU;
        [DisplayProperty(Position = 3)] public object SI;
        [DisplayProperty(Position = 4)] public object Name;
    }

    [FormatProxy(typeof(Process))]
    internal abstract class ProcessDefaultTableFormatProxy
    {
        [TableColumn(Label = "NPM(K)", Alignment = ColumnAlignment.Right, Width = 7, Position = 0)]
        public static object NPM(Process process) { return process.NonpagedSystemMemorySize64 / 1024; }

        [TableColumn(Label = "PM(M)", Alignment = ColumnAlignment.Right, Width = 8, Position = 1)]
        public static object PM(Process process) { return (process.PagedMemorySize64 / (1024*1024)).ToString("N2"); }

        [TableColumn(Label = "WS(M)", Alignment = ColumnAlignment.Right, Width = 10, Position = 2)]
        public static object WS(Process process) { return (process.WorkingSet64 / (1024*1024)).ToString("N2"); }

        [TableColumn(Label = "CPU(s)", Alignment = ColumnAlignment.Right, Width = 10, Position = 3)]
        public static object CPU(Process process) { return process.TotalProcessorTime.TotalSeconds.ToString("N2"); }

        [TableColumn(Alignment = ColumnAlignment.Right, Width = 7, Position = 4)]
        public object Id;

        [TableColumn(Alignment = ColumnAlignment.Right, Width = 3, Position = 5)]
        public object SI;

        [TableColumn(Alignment = ColumnAlignment.Left, Position = 6)]
        public object ProcessName;
    }
}

