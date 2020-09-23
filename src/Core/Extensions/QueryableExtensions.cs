using Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Extensions
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> 扩展
    /// </summary>
    public static partial class QueryableExtensions
    {
        #region WhereIf(是否执行指定条件的查询)

        #endregion

        #region PageBy(分页)
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static PageResult<IQueryable<T>> ToPage<T>(this IQueryable<T> source, int total)
        {
            PageResult<IQueryable<T>> obj = new PageResult<IQueryable<T>>();
            if (source.Count() > 0)
            {
                obj.Result = source;
                obj.Total = total;
            }
            return obj;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public static PageResult<IQueryable<T>> ToPage<T, TKey>(this IQueryable<T> source, int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderByLambda, bool isAsc)
        {
            PageResult<IQueryable<T>> obj = new PageResult<IQueryable<T>>();

            if (source.Count() > 0)
            {
                var tempData = source.Where(whereLambda);
                int total = tempData.Count();

                if (isAsc)
                {
                    tempData = tempData.OrderBy(orderByLambda).
                          Skip(pageSize * (pageIndex - 1)).
                          Take(pageSize);
                }
                else
                {
                    tempData = tempData.OrderByDescending(orderByLambda).
                         Skip(pageSize * (pageIndex - 1)).
                         Take(pageSize);
                }
                obj.Total = total;
                obj.Result = tempData;
            }
            return obj;
        }
        #endregion

    }
}
