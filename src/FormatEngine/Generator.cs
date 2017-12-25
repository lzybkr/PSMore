
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PSMore.Formatting
{
    static class Generator
    {
        public static Descriptor Generate(SelectionCriteria criteria)
        {
            return new BasicDescriptor(criteria.Type);
        }

        struct OrderedProperty
        {
            public string Name;
            public int Position;
            public bool Default;
        }

        public static void Generate(Type type, Type proxyOf, ICondition when, List<Descriptor> descriptors)
        {
            List<OrderedProperty> defaultProperties = new List<OrderedProperty>();

            foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!(member is PropertyInfo) && !(member is FieldInfo)) continue;

                bool sawDefaultDisplay = false;
                foreach (var cad in member.GetCustomAttributesData())
                {
                    if (cad.AttributeType == typeof(DisplayPropertyAttribute))
                    {
                        // Compilers should make these checks unnecessary, but compilers could have bugs.
                        if (cad.ConstructorArguments.Count != 0) continue; // We only have a default ctor.
                        if (sawDefaultDisplay) continue; // Ignore multiple attributes.

                        object position = null;
                        object isDefault = null;
                        if (cad.NamedArguments != null)
                        {
                            foreach (var na in cad.NamedArguments)
                            {
                                if (na.MemberName.Equals(nameof(DisplayPropertyAttribute.Position), StringComparison.Ordinal))
                                {
                                    position = na.TypedValue.Value;
                                    continue;
                                }

                                if (na.MemberName.Equals(nameof(DisplayPropertyAttribute.Default), StringComparison.Ordinal))
                                {
                                    isDefault = na.TypedValue.Value;
                                    continue;
                                }
                            }
                        }

                        sawDefaultDisplay = true;
                        defaultProperties.Add(new OrderedProperty
                        {
                            Name = member.Name,
                            Position = position is int p ? p : -1,
                            Default = isDefault is bool b && b,
                        });
                    }
                }
            }

            var sortedEntries = defaultProperties.OrderBy(op => op.Position).Select(op => new ListDescriptorPropertyEntry(op.Name));
            descriptors.Add(new ListDescriptor(sortedEntries, proxyOf, when));
        }
    }
}
