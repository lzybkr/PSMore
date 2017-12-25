using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PSMore.Formatting
{
    /// <summary>
    /// Exposes a method that determines if formatting should be applied
    /// to the specified object.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Determines if formatting should be applied to the specified object.
        /// </summary>
        bool Applies(object obj);
    }

    /// <summary>
    /// The base class for all formatting descriptors.
    ///
    /// A descriptor describes how to format and also generates the code necessary
    /// to apply the format description.
    /// </summary>
    public abstract class Descriptor
    {
        /// <summary>
        /// The name corresponds to the <c>View</c> parameter in <c>Format-*</c>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type where this type is applicable.
        /// An object wrapped in a PSObject is handled automatically, so this value should
        /// not be PSObject unless it applies to a PSCustomObject.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// A descriptor is normally selected by type, but this can be refined
        /// with an arbitrary condition.
        /// </summary>
        public ICondition When { get; }

        /// <summary>
        /// Initialize the base properties.
        /// </summary>
        protected Descriptor(string name, Type type, ICondition when = null)
        {
            Name = name;
            Type = type;
            When = when;
        }

        /// <summary>
        /// A descriptor is immutable and can be defined without any specific type,
        /// but code generation requires a concrete type, so when adding the type,
        /// the descriptor is cloned.
        /// </summary>
        /// <param name="type">The value to set <see cref="Descriptor.Type"/> in the clone.</param>
        /// <returns>A clone of the <see cref="Descriptor"/> with <see cref="Descriptor.Type"/> set.</returns>
        internal abstract Descriptor Clone(Type type);

        /// <summary>
        /// Descriptors generate expressions that evaluate the object and it's properties
        /// and return formatting instructions, e.g. strings or a header/footer.
        /// </summary>
        internal abstract Expression Bind(Expression toFormat, Expression criteria, LabelTarget returnLabel);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (null == obj) return false;
            if (obj.GetType() != this.GetType()) return false;

            var other = (Descriptor)obj;
            return other.Type == Type && other.When == When &&
                   Name == null || Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get the hash code for the descriptor.
        /// </summary>
        public override int GetHashCode()
        {
            return Utils.CombineHashCodes(
                Name?.GetHashCode() ?? 0,
                When?.GetHashCode() ?? 0,
                Type?.GetHashCode() ?? 0);
        }
    }

    /// <summary>
    /// No special formatting => instead rely on ToString.
    /// </summary>
    public class BasicDescriptor : Descriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicDescriptor"/> class.
        /// </summary>
        /// <param name="type">Value of <see cref="Descriptor.Type"/></param>
        public BasicDescriptor(Type type) : base("Default", type) { }

        internal override Descriptor Clone(Type type) => new BasicDescriptor(type);

        private static readonly MethodInfo ToStringMethodInfo = typeof(object).GetMethod("ToString", Type.EmptyTypes);

        internal override Expression Bind(Expression toFormat, Expression criteria, LabelTarget returnLabel)
        {
            return Expression.IfThen(
                SelectionCriteria.GetCompatibleCall(criteria, this, toFormat),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string),
                        Expression.Call(Expression.Convert(toFormat, typeof(object)), ToStringMethodInfo))));
        }
    }

    /// <summary>
    /// Describes a single row in a ListDescriptor.
    /// </summary>
    public abstract class ListDescriptorEntry
    {
        internal abstract string GetLabel();
        internal abstract Expression GetBinding(Expression target);
    }

    /// <summary>
    /// A list entry that is specified by a property name.
    /// </summary>
    public class ListDescriptorPropertyEntry : ListDescriptorEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDescriptorPropertyEntry"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property for this entry.</param>
        public ListDescriptorPropertyEntry(string propertyName)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; }

        internal override string GetLabel() { return PropertyName; }

        internal override Expression GetBinding(Expression target)
        {
            var binder = FormatGetMemberBinder.Get(PropertyName);
            return Expression.Dynamic(binder, typeof(object), target);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is ListDescriptorPropertyEntry other)) return false;

            return string.Equals(PropertyName, other.PropertyName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get the hash code for the entry.
        /// </summary>
        public override int GetHashCode()
        {
            return PropertyName.GetHashCode();
        }
    }

    /// <summary>
    /// A list entry that is specified by a function that computes the value and a label.
    /// </summary>
    public class ListDescriptorComputedPropertyEntry : ListDescriptorEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDescriptorComputedPropertyEntry"/> class.
        /// </summary>
        /// <param name="expression">
        /// A delegate that is passed the object and returns a string value for this entry.
        /// </param>
        /// <param name="label">
        /// The label to use for this entry.
        /// </param>
        public ListDescriptorComputedPropertyEntry(Func<object, string> expression, string label)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Label = label ?? throw new ArgumentNullException(nameof(label));
        }

        /// <summary>
        /// An optional label for the entry, only used for computed rows.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// The function that computes the value of the entry.
        /// </summary>
        public Func<object, string> Expression { get; }

        internal override string GetLabel() => Label;

        internal override Expression GetBinding(Expression target)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is ListDescriptorComputedPropertyEntry other)) return false;

            return ReferenceEquals(Expression, other.Expression) &&
                Label.Equals(other.Label, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Get the hash code for the entry.
        /// </summary>
        public override int GetHashCode()
        {
            return Utils.CombineHashCodes(Label.GetHashCode(), Expression.GetHashCode());
        }
    }

    /// <summary>
    /// A formatting descriptor that (normally) has 1 row per property.
    /// </summary>
    public class ListDescriptor : Descriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListDescriptor"/> class.
        /// </summary>
        /// <param name="entries">Description of each row.</param>
        /// <param name="type">Value of <see cref="Descriptor.Type"/></param>
        /// <param name="when">Value of <see cref="Descriptor.When"/></param>
        /// <param name="name">Value of <see cref="Descriptor.Name"/></param>
        public ListDescriptor(
            IEnumerable<ListDescriptorEntry> entries = null,
            Type                             type    = null,
            ICondition                       when    = null,
            string                           name    = "Default")
            : base(name, type, when)
        {
            this.Entries = new ReadOnlyCollection<ListDescriptorEntry>(
                entries?.ToArray() ?? Array.Empty<ListDescriptorEntry>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDescriptor"/> class.
        /// </summary>
        /// <param name="properties">Description of each row.</param>
        /// <param name="type">Value of <see cref="Descriptor.Type"/></param>
        /// <param name="when">Value of <see cref="Descriptor.When"/></param>
        /// <param name="name">Value of <see cref="Descriptor.Name"/></param>
        public ListDescriptor(
            object[]   properties,
            Type       type = null,
            ICondition when = null,
            string     name = "Default"
        )
            : base(name, type, when)
        {
            if (properties == null || properties.Length == 0)
                throw new ArgumentException();

            var entries = new List<ListDescriptorEntry>(properties.Length);
            foreach (object p in properties)
            {
                switch (p)
                {
                    case string propName:
                        entries.Add(new ListDescriptorPropertyEntry(propName));
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            this.Entries = new ReadOnlyCollection<ListDescriptorEntry>(entries);
        }

        /// <summary>
        /// Gets the descriptors for each row in the <see cref="ListDescriptor"/>.
        /// </summary>
        public ReadOnlyCollection<ListDescriptorEntry> Entries { get; }

        /// <inheritdoc/>
        internal override Descriptor Clone(Type type)
            => new ListDescriptor(this.Entries, type, this.When, this.Name);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            var other = (ListDescriptor)obj;
            if (Entries.Count != other?.Entries.Count) return false;

            for (int i = 0; i < Entries.Count; i++)
            {
                if (!Entries[i].Equals(other.Entries[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Get the hash code for the descriptor.
        /// </summary>
        public override int GetHashCode()
        {
            var result = base.GetHashCode();
            foreach (var entry in Entries)
            {
                result = Utils.CombineHashCodes(result, entry.GetHashCode());
            }
            return result;
        }

        private static string FormatLine(string formatExpr, string propertyName, object property)
        {
            var propertyAsString = (property == null ? "<null>" : property as string) ?? property.ToString();
            return string.Format(formatExpr, propertyName, propertyAsString);
        }

        private static readonly MethodInfo FormatLineMethodInfo =
            typeof(ListDescriptor).GetMethod(nameof(FormatLine), BindingFlags.NonPublic | BindingFlags.Static);

        internal override Expression Bind(Expression toFormat, Expression criteria, LabelTarget returnLabel)
        {
            int maxLabel = -1;
            foreach (var entry in Entries)
            {
                var label = entry.GetLabel();
                maxLabel = Math.Max(maxLabel, label.Length);
            }
            var formatExpr = "{0,-" + maxLabel + "} : {1}";
            var expressions = new Expression[Entries.Count];
            for (var i = 0; i < Entries.Count; i++)
            {
                var entry = Entries[i];
                expressions[i] = Expression.Call(FormatLineMethodInfo,
                    Expression.Constant(formatExpr),
                    Expression.Constant(entry.GetLabel()),
                    entry.GetBinding(toFormat));
            }

            return Expression.IfThen(
                SelectionCriteria.GetCompatibleCall(criteria, this, toFormat),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string), expressions)));
        }
    }
}