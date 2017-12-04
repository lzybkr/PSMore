using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PSMore.Formatting
{
    public class FormatToString : FormatDirective
    {
        public FormatToString() : base("Default", null) { }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object obj) => ReferenceEquals(obj, this) || base.Equals(obj as FormatToString);

        private static readonly MethodInfo _toString = typeof(object).GetMethod("ToString", Type.EmptyTypes);

        internal override Expression Bind(Expression toFormat, Expression directive, LabelTarget returnLabel)
        {
            return Expression.IfThen(
                Expression.Call(typeof(object).GetMethod("Equals", BindingFlags.Static | BindingFlags.Public),
                    Expression.Constant(this), directive),
                Expression.Return(returnLabel,
                    Expression.NewArrayInit(typeof(string),
                        Expression.Call(Expression.Convert(toFormat, typeof(object)), _toString))));
        }
    }
}
