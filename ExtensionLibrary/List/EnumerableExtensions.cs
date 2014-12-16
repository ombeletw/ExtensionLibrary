using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExtensionLibrary.List
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

        private static readonly MethodInfo GetValueMethod =
       (from m in typeof(PropertyInfo).GetMethods()
        where m.Name == "GetValue" && !m.IsAbstract
        select m).First();

        private static readonly ConstantExpression NullObjectArrayExpression =
            Expression.Constant(null, typeof(object[]));

        public static IEnumerable Transpose<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return TransposeCore(source);
        }

        private static Delegate CreateSelectorFunc<T>(IEnumerable<T> source)
        {
            T[] list = source.ToArray();
            DynamicProperty[] dynamicProperties =
                list.Select(i => new DynamicProperty(i.ToString(), typeof(object))).ToArray();

            Type transposedType = ClassFactory.Instance.GetDynamicClass(dynamicProperties);

            ParameterExpression propParam = Expression.Parameter(typeof(PropertyInfo), "prop");

            var bindings = new MemberBinding[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                MethodCallExpression getter =
                    Expression.Call(
                        propParam,
                        GetValueMethod,
                        Expression.Constant(list[i]),
                        NullObjectArrayExpression
                        );

                bindings[i] = Expression.Bind(transposedType.GetProperty(dynamicProperties[i].Name), getter);
            }

            LambdaExpression selector =
                Expression.Lambda(
                    Expression.MemberInit(
                        Expression.New(transposedType),
                        bindings),
                    propParam);

            return selector.Compile();
        }

        private static IEnumerable TransposeCore<T>(IEnumerable<T> source)
        {
            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            Delegate selector = CreateSelectorFunc(source);

            foreach (PropertyInfo property in properties)
            {
                yield return selector.DynamicInvoke(property);
            }
        }
    }
}
