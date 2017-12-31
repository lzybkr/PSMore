using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (args.Length != 2 || !(args[1] is SelectionCriteria criteria))
            {
                throw new ArgumentException();
            }

            var toFormatExpr = parameters[0];
            var criteriaExpr = parameters[1];

            var descriptor = criteria.Descriptor
                ?? Selector.FindDirective(criteria, args[0])
                ?? Generator.Generate(criteria);

            switch (descriptor)
            {
                case BasicDescriptor bd:
                    return BindDescriptor(bd, toFormatExpr, criteriaExpr, returnLabel);

                case ListDescriptor ld:
                    return BindDescriptor(ld, toFormatExpr, criteriaExpr, returnLabel);

                case TableDescriptor td:
                    return BindDescriptor(td, toFormatExpr, criteriaExpr, returnLabel);
            }

            throw new InvalidOperationException();
        }

        private static readonly MethodInfo GetBasicFormattedResultMethodInfo
            = typeof(RuntimeHelpers).GetMethod(nameof(RuntimeHelpers.GetBasicFormattedResult), BindingFlags.Static | BindingFlags.NonPublic);

        private Expression BindDescriptor(
            BasicDescriptor descriptor,
            Expression toFormat,
            Expression criteria,
            LabelTarget returnLabel)
        {
            return Expression.IfThen(
                SelectionCriteria.GetCompatibleCall(criteria, descriptor, toFormat),
                Expression.Return(returnLabel,
                        Expression.Call(GetBasicFormattedResultMethodInfo, Expression.Convert(toFormat, typeof(object)))));
        }

        static readonly MethodInfo GetPropertyLineFormattedResultMethodInfo =
            typeof(RuntimeHelpers).GetMethod(nameof(RuntimeHelpers.GetPropertyLineFormattedResult), BindingFlags.NonPublic | BindingFlags.Static);

        Expression BindDescriptor(
            ListDescriptor descriptor,
            Expression toFormat,
            Expression criteria,
            LabelTarget returnLabel)
        {
            Expression GetPropertyBinding(ListDescriptorEntry entry)
            {
                switch (entry)
                {
                    case ListDescriptorPropertyEntry ldpe:
                        var binder = FormatGetMemberBinder.Get(ldpe.PropertyName);
                        return Expression.Dynamic(binder, typeof(object), toFormat);

                    case ListDescriptorComputedPropertyEntry ldcpe:
                        throw new NotImplementedException();
                }

                throw new InvalidOperationException();
            }

            int maxLabel = -1;
            foreach (var entry in descriptor.Entries)
            {
                var label = entry.GetLabel();
                maxLabel = Math.Max(maxLabel, label.Length);
            }
            var formatExpr = "{0,-" + maxLabel + "} : {1}";
            var expressions = new Expression[descriptor.Entries.Count];
            for (var i = 0; i < expressions.Length; i++)
            {
                var entry = descriptor.Entries[i];
                expressions[i] = Expression.Call(GetPropertyLineFormattedResultMethodInfo,
                    Expression.Constant(formatExpr),
                    Expression.Constant(entry.GetLabel()),
                    GetPropertyBinding(entry));
            }

            return Expression.IfThen(
                SelectionCriteria.GetCompatibleCall(criteria, descriptor, toFormat),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(FormatInstruction), expressions)));
        }

        static readonly MethodInfo NewEmitTableRowMethodInfo =
            typeof(RuntimeHelpers).GetMethod(nameof(RuntimeHelpers.NewEmitTableRow), BindingFlags.NonPublic | BindingFlags.Static);

        Expression BindDescriptor(
            TableDescriptor descriptor,
            Expression toFormat,
            Expression criteria,
            LabelTarget returnLabel)
        {
            var expressions = new Expression[descriptor.Columns.Count];
            for (var i = 0; i < expressions.Length; i++)
            {
                var col = descriptor.Columns[i];
                if (col.Method != null)
                {
                    var type = descriptor.Type;
                    expressions[i] = Expression.Dynamic(
                        ToStringBinder.Instance,
                        typeof(string),
                        Expression.TryCatch(
                            Expression.Call(col.Method,
                                Expression.Convert(
                                    Expression.Property(
                                        Expression.Convert(toFormat, typeof(PSObject)), "BaseObject"),
                                    type)),
                            Expression.Catch(typeof(Exception), Expression.Default(col.Method.ReturnType))));
                    continue;
                }

                var binder = FormatGetMemberBinder.Get(col.Property);
                expressions[i] = Expression.Dynamic(
                    ToStringBinder.Instance,
                    typeof(string),
                    Expression.Dynamic(binder, typeof(object), toFormat));
            }

            return Expression.IfThen(
                SelectionCriteria.GetCompatibleCall(criteria, descriptor, toFormat),
                Expression.Return(returnLabel,
                    Expression.Call(NewEmitTableRowMethodInfo,
                        Expression.Constant(descriptor),
                        Expression.NewArrayInit(typeof(string), expressions))));
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

    internal class ToStringBinder : ConvertBinder
    {
        private ToStringBinder() : base(typeof(string), @explicit: true)
        {
        }

        internal static readonly ToStringBinder Instance = new ToStringBinder();
        private static MethodInfo ToStringMethodInfo = typeof(object).GetMethod("ToString", Type.EmptyTypes);

        public override DynamicMetaObject FallbackConvert(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
        {
            if (target.Value == null)
            {
                return new DynamicMetaObject(
                    Expression.Constant(""),
                    BindingRestrictions.GetExpressionRestriction(Expression.Equal(
                        Expression.Constant(null),
                        target.Expression)));
            }

            var expr = target.LimitType == typeof(string)
                ? (Expression)Expression.Convert(target.Expression, typeof(string))
                : Expression.Call(Expression.Convert(target.Expression, typeof(object)), ToStringMethodInfo);

            return new DynamicMetaObject(
                expr,
                BindingRestrictions.GetTypeRestriction(target.Expression, target.LimitType));
        }
    }
}
