
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PSMore.FormatAttributes;

namespace PSMore.Formatting
{
    class FormatGenerator
    {
        public static FormatDirective Generate(Type type)
        {
            return new FormatToString();
        }


        struct OrderedProperty
        {
            public string Name;
            public int Position;
        }

        public static void Generate(Type type, List<FormatDirective> directives)
        {
            List<OrderedProperty> defaultProperties = new List<OrderedProperty>();

            foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!(member is PropertyInfo) && !(member is FieldInfo)) continue;

                bool sawDefaultDisplay = false;
                foreach (var cad in member.GetCustomAttributesData())
                {
                    if (cad.AttributeType == typeof(DefaultDisplayPropertyAttribute))
                    {
                        // All of these checks shouldn't be necessary, the compiler
                        // should enforce, but in case they don't, just ignore the attribute.
                        if (cad.ConstructorArguments.Count != 1) continue;
                        var position = cad.ConstructorArguments[0].Value;
                        if (!(position is int)) continue;
                        if (sawDefaultDisplay) continue;

                        sawDefaultDisplay = true;
                        defaultProperties.Add(new OrderedProperty
                        {
                            Name = member.Name, Position = (int)position
                        });
                    }
                }
            }

            directives.Add(
                new ListFormat(defaultProperties.OrderBy(op => op.Position).Select(op => new ListEntry(op.Name))));
        }
    }
}
