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
                Debug.Assert(this == Instance0, "Use Instance0 when passing 1 args");
                return BindAfterFindingDirective(args[0], parameters[0], returnLabel);
            }

            if (args.Length == 2)
            {
                Debug.Assert(this == Instance1, "Use Instance0 when passing 2 args");
                return BindWithDirective(args[1] as FormatDirective, parameters[0], parameters[1], returnLabel);
            }

            throw new Exception();
        }

        private static Expression BindWithDirective(
            FormatDirective directive,
            Expression toFormatExpr,
            Expression directiveExpr,
            LabelTarget returnLabel)
        {
            switch (directive)
            {
                case ListFormat listFormat:
                {
                    return listFormat.Bind(toFormatExpr, directiveExpr, returnLabel);
                }
            }
            throw new Exception();
        }

        private static Expression BindAfterFindingDirective(
            object toFormat,
            Expression toFormatExpr,
            LabelTarget returnLabel)
        {
            var criteria = new FormatSelectionCriteria
            {
                Type = toFormat is PSObject psobj
                    ? psobj.BaseObject.GetType()
                    : toFormat.GetType()
            };

            var directive = FormatSelector.FindDirective(criteria, toFormat)
                ?? FormatGenerator.Generate(criteria.Type);

            return directive.Bind(toFormatExpr, Expression.Constant(directive), returnLabel);
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
