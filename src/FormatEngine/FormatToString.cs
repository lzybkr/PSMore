using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PSMore.Formatting
{
    public class FormatToString : FormatDirective
    {
        public FormatToString(Type type) : base("Default", type, null) { }

        internal override FormatDirective Clone(Type type)
        {
            return new FormatToString(type);
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(obj, this) || base.Equals(obj as FormatToString);

        private static readonly MethodInfo _toString = typeof(object).GetMethod("ToString", Type.EmptyTypes);

        internal override Expression Bind(Expression toFormat, Expression criteria, LabelTarget returnLabel)
        {
            return Expression.IfThen(
                FormatSelectionCriteria.GetCompatibleCall(criteria, this, toFormat),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string),
                        Expression.Call(Expression.Convert(toFormat, typeof(object)), _toString))));
        }
    }
}
