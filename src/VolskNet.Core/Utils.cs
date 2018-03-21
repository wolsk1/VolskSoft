namespace VolskSoft.Bibliotheca
{
    using System;
    using System.Reflection;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    
    public static class Utils
    {
        /// <summary>Produces the set difference of two sequences by using Equals(obj A, obj B) comparer to compare values.</summary>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <typeparam name="TObject">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TObject> Difference<TObject>(this IEnumerable<TObject> first, IEnumerable<TObject> second)
        {
            var difference = new Collection<TObject>();
            var firstItems = first as IList<TObject> ?? first.ToList();
            var secondItems = second as IList<TObject> ?? second.ToList();

            foreach (var firstItem in firstItems)
            {
                if (secondItems.Any(s => s.Equals(firstItem)))
                {
                    continue;
                }

                difference.Add(firstItem);
            }

            return difference;
        }

        /// <summary>
        /// Gets the object property names.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static List<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags = BindingFlags.Public)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            //TODO extract property info extraction in seperate method
            var propertyInfos = type.GetProperties(bindingFlags);

            if (propertyInfos.Any())
            {
                return propertyInfos.ToList();
            }

            return type.GetRuntimeProperties()
                .ToList();
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="classObject">The class object.</param>
        /// <param name="propName">Name of the property.</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object classObject, string propName)
        {
            if (classObject == null)
            {
                throw new ArgumentNullException(nameof(classObject));
            }

            if (string.IsNullOrEmpty(propName))
            {
                throw new ArgumentNullException(nameof(propName));
            }

            var type = classObject.GetType();
            var propertyInfo = type.GetProperty(propName);

            return propertyInfo?.GetValue(classObject);
        }
    }
}