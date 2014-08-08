using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionLibrary.General
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Checks if given varaible is between other 2 variables given
        /// </summary>
        /// <typeparam name="T">the Type</typeparam>
        /// <param name="actual">Value to check</param>
        /// <param name="lower">Low value</param>
        /// <param name="upper">High value</param>
        /// <returns></returns>
        public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }
    }
}
