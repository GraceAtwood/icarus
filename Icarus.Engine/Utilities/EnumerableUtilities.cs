using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Icarus.Engine.Utilities
{
    /// <summary>
    /// Extends the IEnumerable interface.
    /// </summary>
    public static class EnumerableUtilities
    {
        /// <summary>
        /// Casts all objects to the given type.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList Cast(this IEnumerable<object> source, Type type)
        {
            var generic = typeof(List<>).MakeGenericType(type);
            var typedList = (IList) Activator.CreateInstance(generic);
            foreach (var item in source)
                typedList.Add(item);

            return typedList;
        }

        /// <summary>
        /// Inverse of Any
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) =>
            !source.Any(predicate);
    }
}