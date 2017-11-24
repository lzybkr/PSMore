
using System;

namespace PSMore.FormatAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class FormatProxyAttribute : Attribute
    {
        public FormatProxyAttribute(Type type) {}
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DefaultDisplayPropertyAttribute : Attribute
    {
        public DefaultDisplayPropertyAttribute(int position) { }
    }
}