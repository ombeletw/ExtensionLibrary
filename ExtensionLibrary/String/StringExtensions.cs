/*
 * Author: Wim Ombelets, Nicolas Pierre
 * Date: 2014-05-09
 * https://github.com/ombeletw/ExtensionLibrary
 */

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtensionLibrary.Strings
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
        public static string TrimNonLetters(this string input)
        {
            if (input.IsNotNullOrEmpty())
            {
                var sb = new StringBuilder(input.Length);

                foreach (var item in input)
                {
                    if (char.IsLetter(item))
                        sb.Append(item);
                }

                return sb.ToString();
            }
            return null;
        }

        /// <summary>
        /// Trim given leading char (e.g. 00000000065239656).
        /// </summary>
        /// <param name="input">The String</param>
        /// <param name="charaterToTrim">Char To Trim off</param>
        /// <returns></returns>
        public static string TrimLeadingCharacters(this string input, char charaterToTrim)
        {
            if (input.IsNotNullOrEmpty() && charaterToTrim != null)
                return input.TrimStart(charaterToTrim);
            return null;
        }

        /// <summary>
        /// Determines whether it is a valid email address
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is valid email address] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidEmailAddress(this string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }

        /// <summary>
        /// Send an email using the supplied string.
        /// </summary>
        /// <param name="body">String that will be used i the body of the email.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="sender">The email address from which the message was sent.</param>
        /// <param name="recipient">The receiver of the email.</param> 
        /// <param name="server">The server from which the email will be sent.</param>  
        /// <returns>A boolean value indicating the success of the email send.</returns>
        public static bool Email(this string body, string subject, string sender, string recipient, string server)
        {
            try
            {
                // To
                MailMessage mailMsg = new MailMessage();
                mailMsg.To.Add(recipient);

                // From
                MailAddress mailAddress = new MailAddress(sender);
                mailMsg.From = mailAddress;

                // Subject and Body
                mailMsg.Subject = subject;
                mailMsg.Body = body;

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(server);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not send mail from: " + sender + " to: " + recipient + " thru smtp server: " + server + "\n\n" + ex.Message, ex);
            }

            return true;
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value)
        {
            return ToEnum<T>(value, false);
        }

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <param name="ignorecase">Ignore the case of the string being parsed</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value, bool ignorecase)
        {
            if (value == null)
                throw new ArgumentNullException("Value");

            value = value.Trim();

            if (value.Length == 0)
                throw new ArgumentNullException("Must specify valid information for parsing in the string.", "value");

            Type t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", "T");

            return (T)Enum.Parse(t, value, ignorecase);
        }

        /// <summary>
        /// Toes the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultvalue">The defaultvalue.</param>
        /// <returns></returns>
        public static int ToInteger(this string value, int defaultvalue)
        {
            return (int)ToDouble(value, defaultvalue);
        }
        /// <summary>
        /// Toes the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ToInteger(this string value)
        {
            return ToInteger(value, 0);
        }

        /// <summary>
        /// Toes the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultvalue">The defaultvalue.</param>
        /// <returns></returns>
        public static double ToDouble(this string value, double defaultvalue)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            else return defaultvalue;
        }
        /// <summary>
        /// Toes the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            return ToDouble(value, 0);
        }

        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultvalue">The defaultvalue.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value, DateTime? defaultvalue)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            else return defaultvalue;
        }
        /// <summary>
        /// Toes the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value)
        {
            return ToDateTime(value, null);
        }

        /// <summary>
        /// Converts a string value to bool value, supports "T" and "F" conversions.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A bool based on the string value</returns>
        public static bool? ToBoolean(this string value)
        {
            if (string.Compare("T", value, true) == 0)
            {
                return true;
            }
            if (string.Compare("F", value, true) == 0)
            {
                return false;
            }
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            else return null;
        }

        /// <summary>
        /// Indicates whether the current string matches the supplied wildcard pattern.  Behaves the same
        /// as VB's "Like" Operator.
        /// </summary>
        /// <param name="s">The string instance where the extension method is called</param>
        /// <param name="wildcardPattern">The wildcard pattern to match.  Syntax matches VB's Like operator.</param>
        /// <returns>true if the string matches the supplied pattern, false otherwise.</returns>
        /// <remarks>See http://msdn.microsoft.com/en-us/library/swf8kaxw(v=VS.100).aspx</remarks>
        public static bool IsLike(this string s, string wildcardPattern)
        {
            if (s == null || String.IsNullOrEmpty(wildcardPattern)) return false;
            // turn into regex pattern, and match the whole string with ^$
            var regexPattern = "^" + Regex.Escape(wildcardPattern) + "$";

            // add support for ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                                       .Replace(@"\[", "[")
                                       .Replace(@"\]", "]")
                                       .Replace(@"\?", ".")
                                       .Replace(@"\*", ".*")
                                       .Replace(@"\#", @"\d");

            var result = false;
            try
            {
                result = Regex.IsMatch(s, regexPattern);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(String.Format("Invalid pattern: {0}", wildcardPattern), ex);
            }
            return result;
        }

        /// <summary>
        /// same as python 'join'
        /// </summary>
        /// <typeparam name="T">list type</typeparam>
        /// <param name="separator">string separator </param>
        /// <param name="list">list of objects to be ToString'd</param>
        /// <returns>a concatenated list interleaved with separators</returns>
        public static string Join<T>(this string separator, IEnumerable<T> list)
        {
            var sb = new StringBuilder();
            bool first = true;

            foreach (T v in list)
            {
                if (!first)
                    sb.Append(separator);
                first = false;

                if (v != null)
                    sb.Append(v.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a Subset string starting at the specified start index and ending and the specified end
        /// index.
        /// </summary>
        /// <param name="s">The string to retrieve the subset from.</param>
        /// <param name="startIndex">The specified start index for the subset.</param>
        /// <param name="endIndex">The specified end index for the subset.</param>
        /// <returns>A Subset string starting at the specified start index and ending and the specified end
        /// index.</returns>
        public static string Subsetstring(this string s, int startIndex, int endIndex)
        {
            if (startIndex > endIndex)
            {
                throw new InvalidOperationException("End Index must be after Start Index.");
            }

            if (startIndex < 0)
            {
                throw new InvalidOperationException("Start Index must be a positive number.");
            }

            if (endIndex < 0)
            {
                throw new InvalidOperationException("End Index must be a positive number.");
            }

            return s.Substring(startIndex, (endIndex - startIndex));
        }

        /// <summary>
        /// Finds the specified Start Text and the End Text in this string instance, and returns a string
        /// containing all the text starting from startText, to the begining of endText. (endText is not
        /// included.)
        /// </summary>
        /// <param name="s">The string to retrieve the subset from.</param>
        /// <param name="startText">The Start Text to begin the Subset from.</param>
        /// <param name="endText">The End Text to where the Subset goes to.</param>
        /// <param name="ignoreCase">Whether or not to ignore case when comparing startText/endText to the string.</param>
        /// <returns>A string containing all the text starting from startText, to the begining of endText.</returns>
        public static string Subsetstring(this string s, string startText, string endText, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(startText) || string.IsNullOrEmpty(endText))
            {
                throw new ArgumentException("Start Text and End Text cannot be empty.");
            }
            string temp = s;
            if (ignoreCase)
            {
                temp = s.ToUpperInvariant();
                startText = startText.ToUpperInvariant();
                endText = endText.ToUpperInvariant();
            }
            int start = temp.IndexOf(startText);
            int end = temp.IndexOf(endText, start);
            return Subsetstring(s, start, end);
        }


        /// <summary>
        /// Returns true if the given key is not in the string
        /// </summary>
        /// <param name="input">the string</param>
        /// <param name="value">the key</param>
        /// <returns>True - the given key is in the string.  False otherwise.</returns>
        public static bool DoesNotContain(this string input, string value)
        {
            return !input.Contains(value);
        }

        /// <summary>
        /// Returns true if the given keys are not in the string
        /// </summary>
        /// <param name="input">the string</param>
        /// <param name="value">the key</param>
        /// <returns>True - the given keys are in the string.  False otherwise.</returns>
        public static bool DoesNotContain(this string input, params string[] values)
        {
            bool start = true;
            foreach (string value in values) start &= !input.Contains(value);
            return start;
        }
    }
}
