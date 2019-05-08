using System;

namespace Xilion.Framework.Extensions
{
    /// <summary>
    /// System.Type extension methods.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the given type implements a given interface.
        /// </summary>
        /// <typeparam name="T">Interface type to check.</typeparam>
        /// <param name="type">Type to check.</param>
        /// <returns>true if the type implements the interface, false otherwise.</returns>
        public static bool Implements<T>(this Type type) where T : class
        {
            return typeof (T).IsAssignableFrom(type);
        }
    }
}