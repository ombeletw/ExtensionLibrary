/*
 * Author: Wim Ombelets
 * Date: 2014-05-09
 * https://github.com/ombeletw/ExtensionLibrary
 */

namespace ExtensionLibrary.String
{
    /// <summary>
    /// Extension class pertaining to System.String
    /// </summary>
    public static class StringExtensionClass
    {
        /// <summary>
        /// Provides a more linguitically natural method to determine whether the specified string is null or empty.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}
