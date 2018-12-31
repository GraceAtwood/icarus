using System;

namespace Icarus.Utilities
{
    public static class EqualityHelper
    {
        /// <summary>
        /// Compares the references of two IEquatable interfaces, then executes IEquatable.Equals.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool FullEquals<T>(this T instance, object obj) where T : IEquatable<T>
        {
            return instance == null && obj == null || instance != null 
                   && (ReferenceEquals(instance, obj) || obj is T other && other.Equals(instance));
        }
    }
}