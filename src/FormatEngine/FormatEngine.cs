
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Management.Automation;
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
        protected FormatDirective(string name) { Name = name; }
        public string Name { get; }

        internal abstract Expression Bind(Expression toFormat, Type toFormatType, Expression directive, LabelTarget returnLabel);
    }

}
