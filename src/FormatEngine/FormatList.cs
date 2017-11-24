using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PSMore.Formatting
{
    public class ListEntry
    {
        public ListEntry(string propertyName)
        {
            PropertyName = propertyName;
            Label = null;
        }

        public ListEntry(Func<object, string> expression, string label)
        {
            Expression = expression;
            Label = label;
        }

        public string Label { get; }
        public string PropertyName { get; }
        public Func<object, string> Expression { get; }

        public override bool Equals(object obj)
        {
            var other = obj as ListEntry;
            if (other == null) return false;

            return string.Equals(PropertyName, other.PropertyName)
               && ReferenceEquals(Expression, other.Expression);
        }

        public override int GetHashCode()
        {
            return PropertyName != null ? PropertyName.GetHashCode() : Expression.GetHashCode();
        }
    }

    public class ListFormat : FormatDirective
    {
        public ListFormat(IEnumerable<ListEntry> entries = null, string name = "Default")
            : base(name)
        {
            Entries = new ReadOnlyCollection<ListEntry>(entries?.ToArray() ?? Array.Empty<ListEntry>());
        }

        public ListFormat(object[] properties, string name = "Default")
            : base(name)
        {
            if (properties == null || properties.Length == 0)
                throw new ArgumentException();

            var entries = new List<ListEntry>(properties.Length);
            foreach (object p in properties)
            {
                switch (p)
                {
                    case string propName:
                        entries.Add(new ListEntry(propName));
                        break;

                    default:
                        throw new ArgumentException();
                }
            }

            Entries = new ReadOnlyCollection<ListEntry>(entries);
        }

        public ReadOnlyCollection<ListEntry> Entries { get; }

        private static string FormatLine(string formatExpr, string propertyName, object property)
        {
            var propertyAsString = (property == null ? "<null>" : property as string) ?? property.ToString();
            return string.Format(formatExpr, propertyName, propertyAsString);
        }

        private static readonly MethodInfo FormatLineMethodInfo =
            typeof(ListFormat).GetMethod(nameof(FormatLine), BindingFlags.NonPublic | BindingFlags.Static);

        private static readonly MethodInfo ListFormatEquals =
            typeof(ListFormat).GetMethod(nameof(Equals));

        internal override Expression Bind(Expression toFormat, Type toFormatType, Expression directive, LabelTarget returnLabel)
        {
            int maxLabel = -1;
            foreach (var entry in Entries)
            {
                var label = entry.Label ?? entry.PropertyName;
                maxLabel = Math.Max(maxLabel, label.Length);
            }
            var formatExpr = "{0,-" + maxLabel + "} : {1}";
            var expressions = new Expression[Entries.Count];
            for (var i = 0; i < Entries.Count; i++)
            {
                var entry = Entries[i];
                var binder = FormatGetMemberBinder.Get(entry.PropertyName);
                expressions[i] = Expression.Call(FormatLineMethodInfo,
                    Expression.Constant(formatExpr),
                    Expression.Constant(entry.PropertyName),
                    Expression.Dynamic(binder, typeof(object), toFormat));
            }

            return Expression.IfThen(
                Expression.AndAlso(
                    Expression.TypeEqual(toFormat, toFormatType),
                    Expression.Call(Expression.Constant(this), ListFormatEquals, directive)),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string), expressions)));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;

            var other = obj as ListFormat;
            if (Entries.Count != other?.Entries.Count) return false;

            for (int i = 0; i < Entries.Count; i++)
            {
                if (!Entries[i].Equals(other.Entries[i])) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var result = 0;

            foreach (var entry in Entries)
            {
                result = Utils.CombineHashCodes(result, entry.GetHashCode());
            }
            return result;
        }
    }
}
