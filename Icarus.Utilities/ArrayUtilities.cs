using System;

namespace Icarus.Utilities
{
    /// <summary>
    /// Provides methods for interacting with arrays.
    /// </summary>
    public static class ArrayUtilities
    {
        /// <summary>
        /// Executes the given action taking each position in this array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void Each<T>(this T[,] array, Action<int, int> action)
        {
            for (var x = 0; x < array.GetLength(0); x++)
            for (var y = 0; y < array.GetLength(1); y++)
                action(x, y);
        }

        /// <summary>
        /// Executes the action on each element of this array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void Each<T>(this T[,] array, Action<T> action)
        {
            array.Each((x, y) => action(array[x, y]));
        }

        /// <summary>
        /// Sets each element of the array to the output of the given function.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="function"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetEach<T>(this T[,] array, Func<int, int, T> function)
        {
            array.Each((x, y) =>
            {
                var item = function(x, y);
                array[x, y] = item;
            });
        }

        /// <summary>
        /// Sets each element of the array to the output of the given function, passing the current value in to it first,
        /// </summary>
        /// <param name="array"></param>
        /// <param name="function"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetEach<T>(this T[,] array, Func<T, T> function)
        {
            array.Each((x, y) =>
            {
                var item = function(array[x, y]);
                array[x, y] = item;
            });
        }

        /// <summary>
        /// Sets each element in the array to the given value.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetEach<T>(this T[,] array, T value)
        {
            array.Each((x, y) =>
            {
                array[x, y] = value;
            });
        }
    }
}