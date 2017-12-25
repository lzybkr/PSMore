using PSMore.Formatting;
using System.Management.Automation;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{

    [FormatProxy(typeof(ExtendedTypeDefinition))]
    internal abstract class ExtendedTypeDefinitionFormatProxy
    {
        [DisplayProperty(Position = 0)] public object TypeNames;
        [DisplayProperty(Position = 1)] public object FormatViewDefinition;
    }

    [FormatProxy(typeof(PSModuleInfo))]
    internal abstract class PSModuleInfoFormatProxy
    {
        [DisplayProperty(Position = 0)] public object Name;
        [DisplayProperty(Position = 1)] public object Path;
        [DisplayProperty(Position = 2)] public object Description;
        [DisplayProperty(Position = 3)] public object Guid;
        [DisplayProperty(Position = 4)] public object Version;
        [DisplayProperty(Position = 5)] public object ModuleBase;
        [DisplayProperty(Position = 6)] public object ModuleType;
        [DisplayProperty(Position = 7)] public object PrivateData;
        [DisplayProperty(Position = 8)] public object AccessMode;
        [DisplayProperty(Position = 9)] public object ExportedAliases;
        [DisplayProperty(Position = 10)] public object ExportedCmdlets;
        [DisplayProperty(Position = 11)] public object ExportedFunctions;
        [DisplayProperty(Position = 12)] public object ExportedVariables;
        [DisplayProperty(Position = 13)] public object NestedModules;
    }

    [FormatProxy(typeof(ListControlEntry))]
    internal abstract class ListControlEntryFormatProxy
    {
        [DisplayProperty(Position = 0)] public object Items;
        [DisplayProperty(Position = 1)] public object EntrySelectedBy;
    }

}

