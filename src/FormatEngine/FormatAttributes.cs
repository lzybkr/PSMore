
using System;
using PSMore.Formatting;

namespace PSMore.FormatAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class FormatProxyAttribute : Attribute
    {
        public FormatProxyAttribute(Type type) {}

        public Type When
        {
            get => _when;
            set => _when = value != null && typeof(ICondition).IsAssignableFrom(value)
                ? value
                : throw new ArgumentException();
        }
        Type _when;
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DefaultDisplayPropertyAttribute : Attribute
    {
        public DefaultDisplayPropertyAttribute(int position) { }
    }
}