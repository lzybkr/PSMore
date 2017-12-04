
function Convert-TypeData
{
    param(
        [Parameter(ValueFromPipeline)]
        [System.Management.Automation.Runspaces.TypeData[]]
        $TypeData
    )

    process
    {
        foreach ($t in $TypeData)
        {
            if ($null -eq $t.DefaultDisplayPropertySet -and
                $null -eq $t.DefaultDisplayProperty) { continue }

            if ($t.TypeName -match "Microsoft.Management.Infrastructure.CimInstance#((.*)/(.*))")
            {
                $ns = $matches[2]
                $class = $matches[3]

                $n = 0
                $properties = $(foreach ($p in $t.DefaultDisplayPropertySet.ReferencedProperties)
                {
                    "        [DefaultDisplayProperty($n)]public object $p;"
                    $n += 1;
                }) -join "`n"

                Write-Output @"

    [FormatProxy(typeof(CimInstance), When = typeof(When))]
    internal abstract class CimInstance_${class}_FormatProxy
    {
        private class When : ICondition
        {
            bool ICondition.Applies(object o)
                => CimInstanceBindingRestrictions.Applies(o, "$class", "$ns");
        }

$properties
    }
"@
            }
        }

    }
}