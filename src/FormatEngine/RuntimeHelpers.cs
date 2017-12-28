using System;

namespace PSMore.Formatting
{
    static class RuntimeHelpers
    {
        internal static FormatInstruction GetBasicFormattedResult(object obj)
        {
            return new EmitLine { Line = obj.ToString() };
        }

        internal static EmitLine FormatLine(string formatExpr, string propertyName, object property)
        {
            var propertyAsString = (property == null ? "<null>" : property as string) ?? property.ToString();
            return new EmitPropertyLine { Line = String.Format(formatExpr, propertyName, propertyAsString) };
        }

        internal static EmitTableRow[] NewEmitTableRow(string[] columns)
        {
            return new[] { new EmitTableRow { Columns = columns } };
        }
    }
}


