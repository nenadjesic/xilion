using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xilion.Framework.Data;

namespace Xilion.Framework.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///   Sorts <see cref = "IQueryable" /> entity
        /// </summary>
        /// <typeparam name = "T">Tye of queryable</typeparam>
        /// <param name = "queryable">Original <see cref = "IQueryable" /></param>
        /// <param name = "sortingInfo">Sorting info</param>
        /// <returns>Sorted <see cref = "IQueryable" /></returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, SortingInfo sortingInfo) where T : class
        {
            if (sortingInfo != null)
                queryable = sortingInfo.IsAscending
                                ? queryable.OrderBy(sortingInfo.OrderByProperty)
                                : queryable.OrderByDescending(sortingInfo.OrderByProperty);
            return queryable;
        }

        /// <summary>
        ///   Get paged <see cref = "IQueryable" />
        /// </summary>
        /// <typeparam name = "T">Tye of queryable</typeparam>
        /// <param name = "queryable">Original <see cref = "IQueryable" /></param>
        /// <param name = "pagerInfo">Pager info</param>
        /// <returns>Paged <see cref = "IQueryable" /></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PagerInfo pagerInfo) where T : class
        {
            if (pagerInfo == null)
                pagerInfo = PagerInfo.Unpaged;

            pagerInfo.TotalCount = queryable.Count();

            queryable = queryable.Skip(pagerInfo.StartIndex).Take(pagerInfo.PageSize);

            return queryable;
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        // Code taken from: http://stackoverflow.com/questions/41244/dynamic-linq-orderby
        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof (T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                if (pi == null)
                    throw new ArgumentException(String.Format(
                        "There is no property named '{0}' on type '{1}'", prop, type.Name));

                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            Type delegateType = typeof (Func<,>).MakeGenericType(typeof (T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof (Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof (T), type)
                .Invoke(null, new object[] {source, lambda});

            return (IOrderedQueryable<T>) result;
        }
    }
}