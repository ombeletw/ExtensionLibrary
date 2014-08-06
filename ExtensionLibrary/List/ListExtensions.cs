/*
 * Author: Nicolas Pierre
 * Date: 2014-08-06
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ExtensionLibrary.List
{
    public static class ListExtensions
    {
        /// <summary>
        /// Convert our IList to a DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list) where T : class
        {
            if (list != null)
            {
                Type elementType = typeof(T);
                using (DataTable t = new DataTable())
                {
                    PropertyInfo[] _props = elementType.GetProperties();
                    foreach (PropertyInfo propInfo in _props)
                    {
                        Type _pi = propInfo.PropertyType;
                        Type ColType = Nullable.GetUnderlyingType(_pi) ?? _pi;
                        t.Columns.Add(propInfo.Name, ColType);
                    }
                    foreach (T item in list)
                    {
                        DataRow row = t.NewRow();
                        foreach (PropertyInfo propInfo in _props)
                        {
                            row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                        }
                        t.Rows.Add(row);
                    }
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Convert our IList to a DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>DataSet</returns>
        public static DataSet ToDataSet<T>(this IEnumerable<T> list) where T : class
        {
            if (list != null)
            {
                using (DataSet ds = new DataSet())
                {
                    ds.Tables.Add(list.ToDataTable());
                    return ds;
                }
            }
            return null;
        }
    }
}
