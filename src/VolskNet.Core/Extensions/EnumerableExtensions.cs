namespace VolskSoft.Bibliotheca
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Convers IList to datatable
        /// </summary>
        /// <typeparam name="T">Type of IList</typeparam>
        /// <param name="list">IList with type T</param>
        /// <returns>Filled datatable</returns>
        public static DataTable ConvertTo<T>(this IEnumerable<T> list)
        {
            var table = DataTableUtils.CreateTable<T>();
            var entityType = typeof (T);
            var properties = TypeDescriptor.GetProperties(entityType);

            foreach (var item in list)
            {
                var row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Converts list of type DataRow to IList of type T
        /// </summary>
        /// <typeparam name="T">Type if IList</typeparam>
        /// <param name="rows">IList with Rows</param>
        /// <returns>IList of type T</returns>
        public static IEnumerable<T> ConvertTo<T>(IList<DataRow> rows)
        {
            return rows?.Select(DataTableUtils.CreateItem<T>).ToList();
        }

        /// <summary> Like join from JScript: Returns a string value consisting of all
        /// the elements of an <see cref="IEnumerable"/> concatenated together and separated by the
        /// specified separator character.
        /// </summary>
        /// <example>Here is a typical use of the method:
        /// <code>
        /// ArrayList al = new ArrayList();
        /// al.Add("foo");
        /// al.Add(42);
        /// al.Add(4.2);
        /// Join(al, " : ");
        /// </code>
        /// Join method will return <c>"foo : 42 : 4.2"</c>.
        /// </example>
        /// <param name="items">Items to put in the string.</param>
        /// <param name="separator">What to put between items.</param>
        /// <returns>The string where all <c>items</c> are separated by <c>separator</c>.</returns>
        public static string Join(IEnumerable items, string separator)
        {
            var sb = new StringBuilder();
            var e = items.GetEnumerator();
            for (var prev = false; e.MoveNext(); prev = true)
            {
                if (prev)
                {
                    sb.Append(separator);
                }
                sb.Append(e.Current);
            }

            return sb.ToString();
        }

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
    }
}