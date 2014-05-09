/*
 * Author: Wim Ombelets
 * Date: 2014-05-09
 * https://github.com/ombeletw/ExtensionLibrary
 */

namespace ExtensionLibrary.String
{
    /// <summary>
    /// A class with extension methods pertaining to strings.
    /// </summary>
    public static class String
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
