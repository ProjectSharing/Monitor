using System.Collections.Generic;
using System.Linq;

namespace JQCore.Result
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：PageResultExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：分页扩展类
    /// 创建标识：yjq 2017/9/5 14:45:24
    /// </summary>
    public static class PageResultExtension
    {
        /// <summary>
        /// IPageResult<T>
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="data">待分页的数据</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">页长</param>
        /// <param name="maxPage">最大页数</param>
        /// <returns>分页结果</returns>
        public static IPageResult<T> PageResult<T>(this IEnumerable<T> data, int pageIndex, int pageSize, int? maxPage = null) where T : new()
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize < 0 ? 1 : pageSize;
            int totalCount = data == null ? 0 : data.Count();
            return new PageResult<T>(pageIndex, pageSize, totalCount, data?.Skip((pageIndex - 1) * pageSize).Take(pageSize), maxPageCount: maxPage);
        }

        /// <summary>
        /// 将已分页的结果封装成Pageresult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageResultData">已分页数据</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总数</param>
        /// <returns>分页结果</returns>
        public static IPageResult<T> PageResultData<T>(this IEnumerable<T> pageResultData, int pageIndex, int pageSize, int totalCount) where T : new()
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize < 0 ? 1 : pageSize;
            totalCount = totalCount < 0 ? 1 : totalCount;
            return new PageResult<T>(pageIndex, pageSize, totalCount, pageResultData);
        }

        /// <summary>
        /// 空的分页结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">页长</param>
        /// <param name="totalCount">总数</param>
        /// <returns>分页结果</returns>
        public static IPageResult<T> Empty<T>(int pageIndex, int pageSize, int totalCount = 0) where T : new()
        {
            return PageResultData<T>(null, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// 空的分页结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="totalCount">总数</param>
        /// <returns>分页结果</returns>
        public static IPageResult<T> Empty<T>(int totalCount = 0) where T : new()
        {
            return PageResultData<T>(null, 1, 30, totalCount);
        }
    }
}