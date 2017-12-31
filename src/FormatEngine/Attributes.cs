
using PSMore.Formatting;
using System;

namespace PSMore.FormattingAttributes
{
    /// <summary>
    /// Specifies that a class defines the formatting for another type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class FormatProxyAttribute : Attribute
    {
        /// <summary>
        /// Initialize an instance of the class <see cref="FormatProxyAttribute"/>.
        /// </summary>
        /// <param name="type">The proxy <see cref="Type"/>.</param>
        public FormatProxyAttribute(Type type) {}

        /// <summary>
        /// Specifies a type that implements <see cref="ICondition"/> to select
        /// when this proxy is applicable.
        /// </summary>
        public Type When
        {
            get => _when;
            set => _when = value != null && typeof(ICondition).IsAssignableFrom(value)
                ? value
                : throw new ArgumentException();
        }
        Type _when;
    }

    /// <summary>
    /// Specifies the property or field should be included when formatting instances
    /// of this type (or the proxy type.)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DisplayPropertyAttribute : Attribute
    {
        /// <summary>
        /// Specifies the order this property or field should appear in the formatted output.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Specifies this property or field is the default property for the type or proxy type.
        /// The default type is selected in cases where there is limited space to render the object,
        /// for example when using <c>Format-Wide</c>.
        /// </summary>
        public bool Default { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class TableColumnAttribute : Attribute
    {
        /// <summary>
        /// Specifies the column this property or field should appear in the formatted output.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ColumnAlignment Alignment { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Label { get; set; }
    }
}