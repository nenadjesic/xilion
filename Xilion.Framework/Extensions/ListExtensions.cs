using System;
using System.Collections.Generic;

namespace Xilion.Framework.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="IList{T}"/> interface.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Copies all members from the given enumeration to the destination list.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="destinationList">Destination list.</param>
        /// <param name="itemsToAdd">List of items to add to destination list.</param>
        /// <param name="clearList">Value indicating that the destination list should be cleared before addition.
        /// </param>
        /// <returns>The destination list.</returns>
        public static IList<T> AddRange<T>(
            this IList<T> destinationList, IEnumerable<T> itemsToAdd, bool clearList = false)
        {
            if (destinationList == null)
                throw new ArgumentNullException("destinationList");

            if (clearList) destinationList.Clear();
            if (itemsToAdd == null) return destinationList;

            foreach (T item in itemsToAdd)
                destinationList.Add(item);

            return destinationList;
        }
    }
}