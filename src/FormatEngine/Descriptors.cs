using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
    }

    /// <summary>
    /// Describes a single row in a ListDescriptor.
    /// </summary>
    public abstract class ListDescriptorEntry
    {
        internal abstract string GetLabel();
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
    }

    /// <summary>
    /// Specifies the alignment of items in a column.
    /// </summary>
    public enum ColumnAlignment
    {
        /// <summary>
        /// left of the cell, contents will trail with a ... if exceeded - ex "Display..."
        /// </summary>
        Left,

        /// <summary>
        /// center of the cell
        /// </summary>
        Center,

        /// <summary>
        /// right of the cell, contents will lead with a ... if exceeded - ex "...456"
        /// </summary>
        Right,
    }

    /// <summary>
    /// Describes a single column in a <see cref="TableDescriptor"/>.
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableColumn"/> class.
        /// </summary>
        public TableColumn(
            string          property,
            ColumnAlignment alignment = ColumnAlignment.Left,
            int             width     = 0,
            string          label     = null)
        {
            Property = property;
            Alignment = alignment;
            Width = width;
            Label = label;
        }

        /// <summary>
        /// The name of the property to use for the column.
        /// </summary>
        public string Property { get; }

        /// <summary>
        /// Specifies the alignment within the column.
        /// </summary>
        public ColumnAlignment Alignment { get; }

        /// <summary>
        /// Specifies the maximum width of the column. If &lt;= 0, the column width is calculated.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// The label for the column. Normally this should be the property name, but occasionally it is useful
        /// to specify a shortened form of the property name for narrow columns.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is TableColumn other)) return false;

            return string.Equals(Property, other.Property, StringComparison.OrdinalIgnoreCase)
                && Alignment == other.Alignment
                && Width == other.Width
                && string.Equals(Label, other.Label, StringComparison.Ordinal);
        }

        /// <summary>
        /// Get the hash code for the descriptor.
        /// </summary>
        public override int GetHashCode()
        {
            return Utils.CombineHashCodes(
                Property?.GetHashCode() ?? 0,
                Alignment.GetHashCode(),
                Width.GetHashCode(),
                Label?.GetHashCode() ?? 0);
        }
    }

    /// <summary>
    /// A formatting descriptor that (normally) has 1 row per object.
    /// </summary>
    public class TableDescriptor : Descriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableDescriptor"/> class.
        /// </summary>
        /// <param name="columns">Description of each column.</param>
        /// <param name="type">Value of <see cref="Descriptor.Type"/></param>
        /// <param name="when">Value of <see cref="Descriptor.When"/></param>
        /// <param name="name">Value of <see cref="Descriptor.Name"/></param>
        public TableDescriptor(
            IEnumerable<TableColumn> columns = null,
            string                   name = null,
            Type                     type = null,
            ICondition               when = null)
            : base(name, type, when)
        {
            Columns = new ReadOnlyCollection<TableColumn>(
                columns?.ToArray() ?? Array.Empty<TableColumn>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableDescriptor"/> class.
        /// </summary>
        /// <param name="columns">Description of each column.</param>
        /// <param name="type">Value of <see cref="Descriptor.Type"/></param>
        /// <param name="when">Value of <see cref="Descriptor.When"/></param>
        /// <param name="name">Value of <see cref="Descriptor.Name"/></param>
        public TableDescriptor(
            object[]   columns,
            string     name = null,
            Type       type = null,
            ICondition when = null)
            : base(name, type, when)
        {
            if (columns == null || columns.Length == 0)
                throw new ArgumentException();

            var cols = new List<TableColumn>(columns.Length);
            foreach (object p in columns)
            {
                switch (p)
                {
                    case string propName:
                        cols.Add(new TableColumn(propName));
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            this.Columns = new ReadOnlyCollection<TableColumn>(cols);
        }

        /// <summary>
        /// The columns to output.
        /// </summary>
        public ReadOnlyCollection<TableColumn> Columns { get; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            var other = (TableDescriptor)obj;
            if (Columns.Count != other?.Columns.Count) return false;

            for (int i = 0; i < Columns.Count; i++)
            {
                if (!Columns[i].Equals(other.Columns[i])) return false;
            }

            return true;
        }

        /// <summary>
        /// Get the hash code for the descriptor.
        /// </summary>
        public override int GetHashCode()
        {
            var result = base.GetHashCode();
            foreach (var entry in Columns)
            {
                result = Utils.CombineHashCodes(result, entry.GetHashCode());
            }
            return result;
        }

        internal override Descriptor Clone(Type type)
            => new TableDescriptor(Columns, Name, type, When);
    }
}