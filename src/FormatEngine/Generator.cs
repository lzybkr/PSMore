
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PSMore.FormattingAttributes;

namespace PSMore.Formatting
{
    static class Generator
    {
        public static Descriptor Generate(SelectionCriteria criteria)
        {
            return new BasicDescriptor(criteria.Type);
        }

        struct DisplayPropertyData
        {
            public string Name;
            public MethodInfo Method;
            public int Position;
            public bool Default;
        }

        struct TableColumnData
        {
            public string Property;
            public MethodInfo Method;
            public int Position;
            public int Width;
            public string Label;
            public ColumnAlignment Alignment;
        }

        public static void Generate(Type type, Type proxyOf, ICondition when, List<Descriptor> descriptors)
        {
            List<DisplayPropertyData> defaultProperties = new List<DisplayPropertyData>();
            List<TableColumnData> tableColumns = new List<TableColumnData>();

            foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
            {
                MethodInfo methodInfo = null;
                switch (member)
                {
                    case PropertyInfo pi:
                        if (pi.GetMethod == null
                            || pi.GetMethod.IsStatic
                            || !pi.GetMethod.IsPublic) continue;
                        break;

                    case FieldInfo fi:
                        if (!fi.IsPublic || fi.IsStatic) continue;
                        break;

                    case MethodInfo mi:
                        if (!mi.IsPublic
                            || !mi.IsStatic
                            || mi.ReturnParameter == null
                            || mi.ReturnParameter.ParameterType == typeof(void)
                            || mi.GetParameters().Length != 1)
                            continue;
                        methodInfo = mi;
                        break;

                    default:
                        continue;
                }

                bool sawDefaultDisplay = false;
                bool sawTableColumn = false;
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
                        defaultProperties.Add(new DisplayPropertyData
                        {
                            Name = member.Name,
                            Method = methodInfo,
                            Position = position is int p ? p : -1,
                            Default = isDefault is bool b && b,
                        });
                    }
                    else if (cad.AttributeType == typeof(TableColumnAttribute))
                    {
                        // Compilers should make these checks unnecessary, but compilers could have bugs.
                        if (cad.ConstructorArguments.Count != 0) continue; // We only have a default ctor.
                        if (sawTableColumn) continue; // Ignore multiple attributes.

                        object position = null;
                        object alignment = null;
                        object width = null;
                        object label = null;
                        if (cad.NamedArguments != null)
                        {
                            foreach (var na in cad.NamedArguments)
                            {
                                if (na.MemberName.Equals(nameof(TableColumnAttribute.Position), StringComparison.Ordinal))
                                {
                                    position = na.TypedValue.Value;
                                    continue;
                                }
                                if (na.MemberName.Equals(nameof(TableColumnAttribute.Alignment), StringComparison.Ordinal))
                                {
                                    alignment = na.TypedValue.Value;
                                    continue;
                                }
                                if (na.MemberName.Equals(nameof(TableColumnAttribute.Width), StringComparison.Ordinal))
                                {
                                    width = na.TypedValue.Value;
                                    continue;
                                }
                                if (na.MemberName.Equals(nameof(TableColumnAttribute.Label), StringComparison.Ordinal))
                                {
                                    label = na.TypedValue.Value;
                                    continue;
                                }
                            }
                        }

                        sawTableColumn = true;
                        tableColumns.Add(new TableColumnData
                        {
                            Property = member.Name,
                            Method = methodInfo,
                            Position = position is int p ? p : -1,
                            Alignment = alignment is int ca ? (ColumnAlignment)ca : ColumnAlignment.Right,
                            Width = width is int w ? w : 0,
                            Label = label is string s ? s : null,
                        });
                    }
                }
            }

            if (defaultProperties.Count > 0)
            {
                var sortedEntries = defaultProperties
                    .OrderBy(op => op.Position)
                    .Select(op => new ListDescriptorPropertyEntry(op.Name, op.Method));
                descriptors.Add(new ListDescriptor(sortedEntries, proxyOf, when));
            }

            if (tableColumns.Count > 0)
            {
                var sortedColumns = tableColumns
                    .OrderBy(op => op.Position)
                    .Select(op => new TableColumn(op.Property, op.Method, op.Alignment, op.Width, op.Label));
                descriptors.Add(new TableDescriptor(sortedColumns));
            }
        }
    }
}
