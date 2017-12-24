
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PSMore.FormatAttributes;

namespace PSMore.Formatting
{
    public enum FormatStyle
    {
        Default,
        List,
        Table
    }

    public class FormatSelectionCriteria
    {
        public Type Type { get; }
        public string Name { get; }
        public FormatStyle Style { get; }
        public FormatDirective Directive { get; }

        public FormatSelectionCriteria(Type type, FormatStyle style = FormatStyle.Default, FormatDirective directive = null, string name = null)
        {
            Type = type;
            Style = style;
            Directive = directive;
            Name = name;
        }

        public static Expression GetCompatibleCall(Expression criteria, FormatDirective directive, Expression toFormat)
        {
            return Expression.Call(criteria, CompatibleMethodInfo, Expression.Constant(directive), toFormat);
        }

        private static readonly MethodInfo CompatibleMethodInfo =
            typeof(FormatSelectionCriteria).GetMethod(nameof(CompatibleWithDirective), BindingFlags.NonPublic | BindingFlags.Instance);

        private bool CompatibleWithDirective(FormatDirective directive, object obj)
        {
            if (directive.Type != Type) return false;
            if (directive.When != null && !directive.When.Applies(obj)) return false;

            switch (Style)
            {
                case FormatStyle.List:
                    if (!(directive is ListFormat)) return false;
                    break;
            }

            return Directive == null || directive.Equals(Directive);
        }
    }

    class FormatSelector
    {
        private static readonly Dictionary<Type, List<FormatDirective>> FormatDefinitions =
            new Dictionary<Type, List<FormatDirective>>(100);

        static FormatSelector()
        {
            foreach (var type in typeof(FormatSelector).Assembly.GetTypes())
            {
                foreach (var cad in type.GetCustomAttributesData())
                {
                    if (cad.AttributeType != typeof(FormatProxyAttribute)) continue;
                    if (cad.ConstructorArguments.Count != 1) continue;
                    var proxyOf = cad.ConstructorArguments[0].Value as Type;
                    if (proxyOf == null) continue;

                    ICondition when = null;
                    if (cad.NamedArguments != null && cad.NamedArguments.Count == 1)
                    {
                        if (!cad.NamedArguments[0].MemberName.Equals("When")) continue;
                        var whenType = cad.NamedArguments[0].TypedValue.Value as Type;
                        if (whenType == null) continue;
                        when = Activator.CreateInstance(whenType) as ICondition;
                        if (when == null) continue;
                    }

                    if (!FormatDefinitions.TryGetValue(proxyOf, out var directives))
                    {
                        directives = new List<FormatDirective>();
                        FormatDefinitions.Add(proxyOf, directives);
                    }

                    FormatGenerator.Generate(type, proxyOf, when, directives);
                }
            }
        }

        public static FormatDirective FindDirective(FormatSelectionCriteria criteria, object o)
        {
            var type = criteria.Type;

            lock (FormatDefinitions)
            {
                while (type != null)
                {
                    if (!FormatDefinitions.TryGetValue(type, out var directives))
                    {
                        if (type.IsGenericType)
                        {
                            // Try again with the unspecialized type
                            var generic = type.GetGenericTypeDefinition();
                            FormatDefinitions.TryGetValue(generic, out directives);
                        }
                    }

                    if (directives != null)
                    {
                        foreach (var directive in directives)
                        {
                            switch (criteria.Style)
                            {
                                case FormatStyle.List:
                                    if (!(directive is ListFormat)) continue;
                                    break;
                                case FormatStyle.Table:
                                    //if (!(directive is TableFormat)) continue;
                                    break;
                            }

                            if (directive.When != null && !directive.When.Applies(o))
                                continue;

                            if (!string.IsNullOrEmpty(criteria.Name) &&
                                !criteria.Name.Equals(directive.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            return directive;
                        }
                    }

                    type = type.BaseType;
                }
            }

            return null;
        }

        public static void AddDirective(Type type, FormatDirective directive)
        {
            lock (FormatDefinitions)
            {
                if (!FormatDefinitions.TryGetValue(type, out var directives))
                {
                    directives = new List<FormatDirective>();
                    FormatDefinitions.Add(type, directives);
                }
                directives.Add(directive);
            }
        }
    }
}
