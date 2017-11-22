using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PSMore.Formatting
{
    internal class FormatBinder : CallSiteBinder
    {
        private FormatBinder() { }

        public static FormatBinder Instance0 { get; } = new FormatBinder();
        public static FormatBinder Instance1 { get; } = new FormatBinder();

        public override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
        {
            if (args.Length == 1)
            {
                Debug.Assert(this == Instance0, "Use Instance0 when passing 0 args");

                var criteria = new FormatSelectionCriteria();
                if (args[0] is PSObject psobj)
                {
                    criteria.Type = psobj.BaseObject.GetType();
                }
                else
                {
                    criteria.Type = args[0].GetType();
                }

                var directive = FormatSelector.FindDirective(criteria) ?? FormatGenerator.Generate(criteria.Type);

                return directive.Bind(parameters[0], args[0].GetType(), Expression.Constant(directive), returnLabel);
            }

            if (args.Length == 2)
            {
                Debug.Assert(this == Instance1, "Use Instance0 when passing 0 args");

                switch (args[1])
                {
                    case ListFormat listFormat:
                        return listFormat.Bind(parameters[0], args[0].GetType(), parameters[1], returnLabel);
                }
            }

            throw new Exception();
        }
    }

    internal class FormatGetMemberBinder : GetMemberBinder
    {
        private static readonly ConcurrentDictionary<string, FormatGetMemberBinder> Binders =
            new ConcurrentDictionary<string, FormatGetMemberBinder>(
                2,
                Array.Empty<KeyValuePair<string, FormatGetMemberBinder>>(),
                StringComparer.OrdinalIgnoreCase);

        public static FormatGetMemberBinder Get(string name)
        {
            return Binders.GetOrAdd(name, n => new FormatGetMemberBinder(n));
        }

        private FormatGetMemberBinder(string name) : base(name, true) {}

        public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
        {
            // The dynamic object protocol defers to a dynamic object (IDynamicMetaObjectProvider) before
            // calling this method.
            //
            // The only use of the binder is at a site that always receives a PSObject, so we should never get here.
            throw new NotImplementedException();
        }
    }
}
