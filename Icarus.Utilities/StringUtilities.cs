using System;
using System.Collections.Generic;

namespace Icarus.Utilities
{
    public static class StringUtilities
    {
        public static string Join<T>(this IEnumerable<T> source, string separator)
        {
            return String.Join(separator, source);
        }
    }
}