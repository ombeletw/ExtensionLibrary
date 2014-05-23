/*
 * Author: Wim Ombelets
 * Date: 2014-05-09
 * https://github.com/ombeletw/ExtensionLibrary
 */

using System.Text;

namespace ExtensionLibrary.StringExtensions
{
    /// <summary>
    /// A class with extension methods pertaining to strings.
    /// </summary>
    public static class StringExtensions
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

        /// <summary>
        /// Trim all non letters from a string.
        /// </summary>
        /// <param name="input">The String</param>
        /// <returns>Trimmed String</returns>
        public static string TrimNonLetters(string input)
        {
            var sb = new StringBuilder(input.Length);
            
            foreach (var item in input)
            {
                if (char.IsLetter(item))
                    sb.Append(item);
            }

            return sb.ToString();
        }
    }
}
