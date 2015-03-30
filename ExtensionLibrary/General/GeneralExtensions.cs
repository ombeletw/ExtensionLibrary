using System;
using System.Windows;
using System.Windows.Media;

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

        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
