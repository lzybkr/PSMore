
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PSMore.Formatting
{
    public class FormatEngine
    {
        private static readonly CallSite<Func<CallSite, object, IEnumerable<string>>> _site0 =
            CallSite<Func<CallSite, object, IEnumerable<string>>>.Create(FormatBinder.Instance0);

        private static readonly CallSite<Func<CallSite, object, FormatDirective, IEnumerable<string>>> _site1 =
            CallSite<Func<CallSite, object, FormatDirective, IEnumerable<string>>>.Create(FormatBinder.Instance1);

        public const string AttachedFormatPropertyName = "__PSMoreFormat";

        public static IEnumerable<string> Format(object o)
        {
            var psobj = o as PSObject ?? new PSObject(o);
            var directive = psobj.Properties[AttachedFormatPropertyName]?.Value as FormatDirective;
            return directive != null
                ? _site1.Target.Invoke(_site1, psobj, directive)
                : _site0.Target.Invoke(_site0, psobj);
        }

        public static IEnumerable<string> Format(object o, FormatDirective directive)
        {
            var psobj = o as PSObject ?? new PSObject(o);
            return _site1.Target.Invoke(_site1, psobj, directive);
        }
    }

    public abstract class FormatDirective
    {
        protected FormatDirective(string name, ICondition when)
        {
            Name = name;
            When = when;
        }

        public override bool Equals(object obj)
        {
            var other = obj as FormatDirective;
            return other != null
                && string.Equals(Name, other.Name)
                && object.ReferenceEquals(When, other.When);
        }

        public override int GetHashCode()
        {
            var n1 = Name != null ? Name.GetHashCode() : 0;
            var n2 = When != null ? When.GetHashCode() : 0;
            return Utils.CombineHashCodes(n1, n2);
        }

        public string Name { get; }
        public ICondition When { get; }

        internal abstract Expression Bind(Expression toFormat, Expression directive, LabelTarget returnLabel);

        protected Expression GetAppliesCall(Expression directive, Expression toFormat)
        {
            return Expression.Call(Expression.Constant(this), FormatApplies, directive, toFormat);
        }

        private static readonly MethodInfo FormatApplies =
            typeof(FormatDirective).GetMethod(nameof(Applies), BindingFlags.NonPublic | BindingFlags.Instance);

        private bool Applies(FormatDirective other, object obj)
        {
            return this.Equals(other) && (When == null || When.Applies(obj));
        }
    }

}
