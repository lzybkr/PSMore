
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PSMore.Formatting
{
    public static class FormatEngine
    {
        private static readonly CallSite<Func<CallSite, object, FormatSelectionCriteria, IEnumerable<string>>> _site =
            CallSite<Func<CallSite, object, FormatSelectionCriteria, IEnumerable<string>>>.Create(FormatBinder.Instance);

        public const string AttachedFormatPropertyName = "__PSMoreFormat";

        public static IEnumerable<string> Format(object o)
        {
            return Format(o, directive: null);
        }

        public static IEnumerable<string> Format(object o, FormatDirective directive)
        {
            Type type;
            if (o is PSObject psobj)
            {
                type = psobj.BaseObject.GetType();
            }
            else
            {
                type = o.GetType();
                psobj = new PSObject(o);
            }

            if (directive == null)
            {
                directive = psobj.Properties[AttachedFormatPropertyName]?.Value as FormatDirective;
            }

            FormatStyle style = FormatStyle.Default;
            if (directive != null)
            {
                switch (directive)
                {
                    case ListFormat lf:
                        style = FormatStyle.List;
                        break;
                }

                if (directive.Type == null)
                {
                    directive = directive.Clone(type);
                }
            }

            var criteria = new FormatSelectionCriteria(
                directive: directive,
                style: style,
                type: type,
                name: null);
            return _site.Target.Invoke(_site, psobj, criteria);
        }
    }

    public abstract class FormatDirective
    {
        public string Name { get; }
        public ICondition When { get; }
        public Type Type { get; }

        protected FormatDirective(string name, Type type, ICondition when)
        {
            Name = name;
            Type = type;
            When = when;
        }

        internal abstract FormatDirective Clone(Type type);

        internal abstract Expression Bind(Expression toFormat, Expression criteria, LabelTarget returnLabel);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormatDirective) obj);
        }

        public override int GetHashCode()
        {
            return Utils.CombineHashCodes(
                Name?.GetHashCode() ?? 0,
                When?.GetHashCode() ?? 0);
        }
    }
}
