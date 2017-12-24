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

        public static FormatBinder Instance { get; } = new FormatBinder();

        public override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
        {
            if (args.Length != 2 || !(args[1] is FormatSelectionCriteria criteria))
            {
                throw new ArgumentException();
            }

            var toFormatExpr = parameters[0];
            var criteriaExpr = parameters[1];

            var directive = criteria.Directive
                ?? FormatSelector.FindDirective(criteria, args[0])
                ?? FormatGenerator.Generate(criteria);

            return directive.Bind(toFormatExpr, criteriaExpr, returnLabel);
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
