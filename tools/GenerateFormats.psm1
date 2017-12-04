
function GenerateTypeData
{
    param($TypeData, $StringBuilder)

    $n = 0
    $sawDefault = $null -eq $TypeData.DefaultDisplayProperty
    foreach ($p in $TypeData.DefaultDisplayPropertySet.ReferencedProperties)
    {
        $d = if ($p -eq $TypeData.DefaultDisplayProperty) {
            $sawDefault = $true
            ", Default = true"
        } else { "" }
        $null = $StringBuilder.Append("
        [DisplayProperty(Position = $n$d)] public object $p;")
        $n += 1
    }

    if (!$sawDefault) {
        $null = $StringBuilder.Append("
        [DisplayProperty(Default = true)] public object $($TypeData.DefaultDisplayProperty);")
    }
}

function ConvertCimInstance
{
    param(
        [System.Management.Automation.Runspaces.TypeData]$TypeData,
        [System.Text.StringBuilder]$StringBuilder,
        [string] $Namespace,
        [string] $Class
    )

    $null = $StringBuilder.Append("
    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_${Class}_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, ""$Class"", ""$Namespace"");
        }
")

    GenerateTypeData -TypeData $TypeData -StringBuilder $StringBuilder

    $null = $StringBuilder.Append("
    }
")
}

function ConvertDotNetType
{
    param(
        [System.Management.Automation.Runspaces.TypeData]$TypeData,
        [System.Collections.Generic.Dictionary[string,System.Text.StringBuilder]]$Builders
    )

    $endNs = $TypeData.TypeName.LastIndexOf(".")
    $Namespace = $TypeData.TypeName.Substring(0, $endNs)
    $Class = $TypeData.TypeName.Substring($endNs + 1)

    $sb = $Builders[$Namespace]
    if ($null -eq $sb)
    {
        $sb = [System.Text.StringBuilder]::new()
        $Builders[$Namespace] = $sb
        $null = $sb.Append("using PSMore.FormatAttributes;
using $Namespace;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{
")
    }

    $null = $sb.Append("
    [FormatProxy(typeof($Class))]
    internal abstract class ${Class}FormatProxy
    {")

    GenerateTypeData -TypeData $TypeData -StringBuilder $sb

    $null = $sb.Append("
    }
")
}

function Convert-TypeData
{
    param(
        [Parameter(ValueFromPipeline)]
        [System.Management.Automation.Runspaces.TypeData[]]
        $TypeData
    )

    begin
    {
        $cimInstanceFormatters = [System.Text.StringBuilder]::new()
        $dotNetBuilders = [System.Collections.Generic.Dictionary[string, System.Text.StringBuilder]]::new()
    }

    process
    {
        foreach ($t in $TypeData)
        {
            if ($null -eq $t.DefaultDisplayPropertySet -and
                $null -eq $t.DefaultDisplayProperty) { continue }

            if ($t.TypeName -match "Microsoft.Management.Infrastructure.CimInstance#((.*)/(.*))")
            {
                ConvertCimInstance -TypeData $t -StringBuilder $cimInstanceFormatters -Namespace $matches[2] -Class $matches[3]
                continue
            }

            # Skip some things that we won't take a dependency on.
            if ($t.TypeName -match "System.Management.ManagementObject.*") {continue}
            if ($t.TypeName -notmatch "^System") { continue }

            ConvertDotNetType -TypeData $t -Builders $dotNetBuilders
        }
    }

    end
    {
@"
using System;
using System.Management.Automation;
using PSMore.FormatAttributes;
using Microsoft.Management.Infrastructure;
using PSMore.Formatting;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
#pragma warning disable CS0169 // Ignore internal fields never assigned
#pragma warning disable CS0649 // Ignore internal fields never assigned

namespace PSMore.DefaultFormats
{
    internal class CimInstanceBindingRestrictions
    {
        public static bool Applies(object o, string className, string ns)
        {
            if (o is PSObject psObj) o = psObj.BaseObject;
            var cimInstance = o as CimInstance;
            return cimInstance != null
                && string.Equals(cimInstance.CimSystemProperties.ClassName, className, StringComparison.Ordinal)
                && string.Equals(cimInstance.CimSystemProperties.Namespace, ns, StringComparison.Ordinal);
        }
    }
$cimInstanceFormatters
}
"@ | Out-File -Encoding ascii $PSScriptRoot/../src/DefaultFormats/Microsoft.Management.Infrastructure.Formats.cs

        foreach ($pair in $dotNetBuilders.GetEnumerator())
        {
            $sb = $pair.Value
            $null = $sb.Append("
}
")

            $namespace = $pair.Key
            $sb.ToString() | Out-File -Encoding ascii "$PSScriptRoot/../src/DefaultFormats/$namespace.Formats.cs"
        }
    }
}

Export-ModuleMember Convert-TypeData