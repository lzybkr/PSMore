using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PSMore.Formatting
{
    public class FormatToString : FormatDirective
    {
        private static readonly MethodInfo _toString = typeof(object).GetMethod("ToString", Type.EmptyTypes);

        internal override Expression Bind(Expression toFormat, Type toFormatType, Expression directive, LabelTarget returnLabel)
        {
            return Expression.IfThen(
                Expression.AndAlso(
                    Expression.TypeEqual(toFormat, toFormatType),
                    Expression.TypeEqual(directive, typeof(FormatToString))),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string),
                        Expression.Call(Expression.Convert(toFormat, typeof(object)), _toString))));
        }
    }
}
