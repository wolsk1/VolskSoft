namespace VolskSoft.ExTools
{
    using OfficeOpenXml;
    using VolskSoft.Bibliotheca;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    public static class ExcelUtils
    {
        /// <summary>
        /// Gets the type property names.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <returns></returns>
        public static Collection<string> GetTypePropertyNames(this Type type, BindingFlags bindingFlags = BindingFlags.Public)
        {
            var allProperties = type.GetProperties(bindingFlags);
            var allowedProperties = allProperties.Where(p => !Attribute.IsDefined(p, typeof(ExcelExportIgnore)));

            return new Collection<string>(allowedProperties.Select(propertyInfo => propertyInfo.Name).ToList());
        }

        /// <summary>
        /// Gets the number format.
        /// </summary>
        /// <param name="numberFormat">The number format.</param>
        /// <returns></returns>
        public static string GetNumberFormat(NumberFormat numberFormat)
        {
            var format = CellConfiguration.NumberFormats.FirstOrDefault(f => f.Key == numberFormat);

            return format.Value;
        }

        /// <summary>
        /// Gets the class as headers.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetClassAsHeaders(Type type)
        {
            var propertyNames = type.GetTypePropertyNames();

            return propertyNames.Select(n => n.FromPascalCase());
        }

        /// <summary>
        /// Loads from collection filtered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cellRange">The cell range.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="printHeaders">if set to <c>true</c> [print headers].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// cellRange
        /// or
        /// collection
        /// </exception>
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(
            this ExcelRangeBase cellRange,
            IEnumerable<T> collection,
            bool printHeaders = false)
        {
            if (cellRange == null)
            {
                throw new ArgumentNullException(nameof(cellRange));
            }
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(ExcelExportIgnore)))
                .ToArray();

            return cellRange.LoadFromCollection(collection, printHeaders,
                OfficeOpenXml.Table.TableStyles.None,
                BindingFlags.Instance | BindingFlags.Public,
                membersToInclude);
        }
    }
}