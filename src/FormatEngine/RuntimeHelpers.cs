using System;

namespace PSMore.Formatting
{
    static class RuntimeHelpers
    {
        internal static FormatInstruction[] GetBasicFormattedResult(object obj)
        {
            return new FormatInstruction[] { new EmitLine { Line = obj.ToString() } };
        }

        internal static FormatInstruction GetPropertyLineFormattedResult(string formatExpr, string propertyName, object property)
        {
            var propertyAsString = (property == null ? "" : property as string) ?? property.ToString();
            return new EmitPropertyLine { Line = String.Format(formatExpr, propertyName, propertyAsString) };
        }

        internal static FormatInstruction[] NewEmitTableRow(TableDescriptor descriptor, string[] columns)
        {
            return new FormatInstruction[]
            {
                new EmitTableRow { Descriptor = descriptor,  Columns = columns }
            };
        }
    }
}


