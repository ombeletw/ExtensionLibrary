/*
 * Author: Wim Ombelets
 * Date: 2014-05-09
 * https://github.com/ombeletw/ExtensionLibrary
 */

using System.Collections.Generic;
using System.Linq;

namespace ExtensionLibrary.Math
{
    /// <summary>
    /// A class with extension methods pertaining to prime numbers
    /// </summary>
    public static class MathExtensionClass
    {
        /// <summary>
        /// Determines whether the specified number is a prime.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static bool IsPrime(this long number)
        {
            if (number <= 0)
                return false;
            else
            {
                for (long i = 2; i < number; i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Determines whether the specified number is a prime.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static bool IsPrime(this int number)
        {
            if (number <= 0)
                return false;
            else
            {
                for (int i = 2; i < number; i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Calculates the largest palindrome for two integers.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        public static long LargestPalindrome(int a, int b)
        {
            long largest = 0;
            long normal = 0;
            long reversed = 0;

            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    normal = i * j;
                    reversed = long.Parse(normal.ToString().Reverse().ToString());
                    if (normal == reversed)
                        largest = normal;
                }
            }

            return largest;
        }

        /// <summary>
        /// Gets the factors of a specified number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static HashSet<long> Factors(long number)
        {
            var factors = new HashSet<long>();

            for (int i = 2; i <= number; i++)
            {
                if (number % i == 0)
                {
                    factors.Add(i);
                    number /= i;
                    i--;
                }
                else
                    i = 2;
            }

            return factors;
        }
    }
}
