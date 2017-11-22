
using System;

namespace PSMore.Formatting
{
    class FormatGenerator
    {
        public static FormatDirective Generate(Type type)
        {
            return new FormatToString();
        }
    }
}
