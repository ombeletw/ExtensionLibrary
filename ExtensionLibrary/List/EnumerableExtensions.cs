using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExtensionLibrary.Enumerable
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Gives an typeless IEnumerable the possibilty to check if it contains items
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Any(this IEnumerable source)
        {
            return Count(source) != 0;
        }

        /// <summary>
        /// Gives an typeless IEnumerable the possibilty to return it's count
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int Count(IEnumerable source)
        {
            int res = 0;

            if (source != null)
            {
                foreach (var item in source)
                    res++;
            }

            return res;
        }

        /// <summary>
        /// Checks if generic IEnumerable is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Any();
        }
    }
}
