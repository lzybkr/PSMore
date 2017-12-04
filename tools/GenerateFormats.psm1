
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
    $n = 0
    foreach ($p in $TypeData.DefaultDisplayPropertySet.ReferencedProperties)
    {
        $null = $StringBuilder.Append("
        [DefaultDisplayProperty($n)] public object $p;")
        $n += 1
    }

    $null = $StringBuilder.Append("
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

            # Skip
            if ($t.TypeName -match "System.Management.ManagementObject.*") {continue}
            if ($t.TypeName -notmatch "^System") { continue }

            $t.TypeName
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

    }
}

Export-ModuleMember Convert-TypeData