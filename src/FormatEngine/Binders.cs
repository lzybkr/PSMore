using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PSMore.Formatting
{
    internal class FormatBinder : CallSiteBinder
    {
        private FormatBinder() { }

        public static FormatBinder Instance { get; } = new FormatBinder();

        public override Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel)
        {
            if (args.Length != 2 || !(args[1] is SelectionCriteria criteria))
            {
                throw new ArgumentException();
            }

            var toFormatExpr = parameters[0];
            var criteriaExpr = parameters[1];

            var descriptor = criteria.Descriptor
                ?? Selector.FindDirective(criteria, args[0])
                ?? Generator.Generate(criteria);

            return descriptor.Bind(toFormatExpr, criteriaExpr, returnLabel);
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
