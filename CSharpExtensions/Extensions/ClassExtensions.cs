﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CSharpExtensions.Extensions
{
    public static class ClassExtensions
    {
        public static IEnumerable<Difference> FindDifferences<T>(this T before, T after) where T : class
        {
            #region Validations

            if (before is null)
            {
                throw new ArgumentNullException(nameof(before));
            }

            if (after is null)
            {
                return Enumerable.Empty<Difference>();
            }

            #endregion

            PropertyInfo[] propertiesInfo = before.GetType().GetProperties();
            var changes = new List<Difference>(propertiesInfo.Count());

            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                bool shouldIgnore = Attribute.IsDefined(propertyInfo, typeof(IgnoreDifferenceCheckAttribute));

                if (shouldIgnore)
                {
                    continue;
                }

                object beforeValue = propertyInfo.GetValue(before);
                object afterValue = propertyInfo.GetValue(after);

                if (beforeValue.Equals(afterValue))
                {
                    continue;
                }

                DisplayNameAttribute overrideName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();

                changes.Add(new Difference
                {
                    Name = (overrideName is null) ? propertyInfo.Name : overrideName.DisplayName,
                    Before = beforeValue,
                    After = afterValue
                });
            }

            return changes;
        }
    }

    public class Difference
    {
        public string Name { get; set; }

        public object Before { get; set; }

        public object After { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreDifferenceCheckAttribute : Attribute { }
}
