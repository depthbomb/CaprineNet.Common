using System;

namespace CaprineNet.Common.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns the index of an array item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T value) => Array.IndexOf(array, value);
    }
}
