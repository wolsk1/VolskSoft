namespace VolskSoft.Bibliotheca
{
    using System;
    using System.Collections.Generic;

    public static class LinqExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="T:System.Collections.ObjectModel.Collection`1"/>.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="otherCollection">The other collection.</param>
        public static void AddRange<TObject>(this ICollection<TObject> collection, IEnumerable<TObject> otherCollection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (otherCollection == null)
            {
                throw new ArgumentNullException(nameof(otherCollection));
            }

            foreach (var o in otherCollection)
            {
                collection.Add(o);
            }
        }
    }
}