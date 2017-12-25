
using System;
using System.Collections.Generic;

namespace PSMore.Formatting
{
    class Selector
    {
        private static readonly Dictionary<Type, List<Descriptor>> FormatDefinitions =
            new Dictionary<Type, List<Descriptor>>(100);

        static Selector()
        {
            foreach (var type in typeof(Selector).Assembly.GetTypes())
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

                    if (!FormatDefinitions.TryGetValue(proxyOf, out var descriptors))
                    {
                        descriptors = new List<Descriptor>();
                        FormatDefinitions.Add(proxyOf, descriptors);
                    }

                    Generator.Generate(type, proxyOf, when, descriptors);
                }
            }
        }

        public static Descriptor FindDirective(SelectionCriteria criteria, object o)
        {
            var type = criteria.Type;

            lock (FormatDefinitions)
            {
                while (type != null)
                {
                    if (!FormatDefinitions.TryGetValue(type, out var descriptors))
                    {
                        if (type.IsGenericType)
                        {
                            // Try again with the unspecialized type
                            var generic = type.GetGenericTypeDefinition();
                            FormatDefinitions.TryGetValue(generic, out descriptors);
                        }
                    }

                    if (descriptors != null)
                    {
                        foreach (var descriptor in descriptors)
                        {
                            switch (criteria.Style)
                            {
                                case Style.List:
                                    if (!(descriptor is ListDescriptor)) continue;
                                    break;
                                case Style.Table:
                                    //if (!(descriptor is TableFormat)) continue;
                                    break;
                            }

                            if (descriptor.When != null && !descriptor.When.Applies(o))
                                continue;

                            if (!string.IsNullOrEmpty(criteria.Name) &&
                                !criteria.Name.Equals(descriptor.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            return descriptor;
                        }
                    }

                    type = type.BaseType;
                }
            }

            return null;
        }

        public static void AddDirective(Type type, Descriptor descriptor)
        {
            lock (FormatDefinitions)
            {
                if (!FormatDefinitions.TryGetValue(type, out var descriptors))
                {
                    descriptors = new List<Descriptor>();
                    FormatDefinitions.Add(type, descriptors);
                }
                descriptors.Add(descriptor);
            }
        }
    }
}
