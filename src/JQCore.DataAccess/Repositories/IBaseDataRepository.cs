using JQCore.Result;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JQCore.DataAccess.Repositories
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IBaseDataRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库访问基础类
    /// 创建标识：yjq 2017/9/5 20:44:17
    /// </summary>
    public interface IBaseDataRepository<T> where T : class, new()
    {
        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="infoList">要插入的列表</param>
        void BulkInsert(List<T> infoList);

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="dataTable">要插入的表格</param>
        void BulkInsert(DataTable dataTable);

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="infoList">要插入的列表</param>
        /// <returns>Task</returns>
        Task BulkInsertAsync(List<T> infoList);

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="dataTable">要插入的表格</param>
        /// <returns>Task</returns>
        Task BulkInsertAsync(DataTable dataTable);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        int Delete(object condition);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        Task<int> DeleteAsync(object condition);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        int Delete(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>受影响的行数</returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最小值</returns>
        TReturn Min<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 异步获取最小值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最小值</returns>
        Task<TReturn> MinAsync<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最大值</returns>
        TReturn Max<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 异步获取最大值
        /// </summary>
        /// <typeparam name="TProperty">属性</typeparam>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="expression">属性表达式</param>
        /// <param name="condition">条件</param>
        /// <param name="isWrite">是否为写连接</param>
        /// <returns>最大值</returns>
        Task<TReturn> MaxAsync<TProperty, TReturn>(Expression<Func<T, TProperty>> expression, Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        TDto GetDto<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        Task<TDto> GetDtoAsync<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        TDto GetDto<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步获取传输对象
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        Task<TDto> GetDtoAsync<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        T GetInfo(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        Task<T> GetInfoAsync(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        T GetInfo(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步获取单个对象
        /// </summary>
        /// <param name="condition">获取条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象</returns>
        Task<T> GetInfoAsync(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="infoList">要插入的数据列表</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        void InsertMany(List<T> infoList, string keyName = null, string[] ignoreFields = null, bool isIdentity = true);

        /// <summary>
        /// 异步插入多条数据
        /// </summary>
        /// <param name="infoList">要插入的数据列表</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns></returns>
        Task InsertManyAsync(List<T> infoList, string keyName = null, string[] ignoreFields = null, bool isIdentity = true);

        /// <summary>
        /// 插入单挑数据
        /// </summary>
        /// <param name="info">对象值</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns>如果是自增则返回自增值，不是返回新增行数</returns>
        object InsertOne(T info, string keyName = null, string[] ignoreFields = null, bool isIdentity = true);

        /// <summary>
        /// 异步插入单挑数据
        /// </summary>
        /// <param name="info">对象值</param>
        /// <param name="keyName">主键名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isIdentity">是否自增</param>
        /// <returns>如果是自增则返回自增值，不是返回新增行数</returns>
        Task<object> InsertOneAsync(T info, string keyName = null, string[] ignoreFields = null, bool isIdentity = true);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        int QueryCount(object condition, bool isWrite = false);

        /// <summary>
        /// 异步查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        Task<int> QueryCountAsync(object condition, bool isWrite = false);

        /// <summary>
        /// 异步查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        Task<int> QueryCountAsync(Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>范总数量</returns>
        int QueryCount(Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        IEnumerable<T> QueryList(bool isWrite = false);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        IEnumerable<T> QueryList(object condition, bool isWrite = false);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        IEnumerable<T> QueryList(Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        Task<IEnumerable<T>> QueryListAsync(Expression<Func<T, bool>> condition, bool isWrite = false);

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        IEnumerable<TDto> QueryList<TDto>(string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        IEnumerable<TDto> QueryList<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        IEnumerable<TDto> QueryList<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        Task<IEnumerable<TDto>> QueryListAsync<TDto>(Expression<Func<T, bool>> condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        Task<IEnumerable<T>> QueryListAsync(bool isWrite = false);

        /// <summary>
        /// 异步查询列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>列表</returns>
        Task<IEnumerable<T>> QueryListAsync(object condition, bool isWrite = false);

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        Task<IEnumerable<TDto>> QueryListAsync<TDto>(string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 异步查询传输对象列表
        /// </summary>
        /// <typeparam name="TDto">传输对象类型</typeparam>
        /// <param name="condition">查询条件</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <param name="isWrite">是否为写连接(事务中使用)</param>
        /// <returns>传输对象列表</returns>
        Task<IEnumerable<TDto>> QueryListAsync<TDto>(object condition, string[] ignoreFields = null, bool isWrite = false);

        /// <summary>
        /// 分页查询（目前只支持MSSQLServer）
        /// </summary>
        /// <typeparam name="TModel">分页结果类型</typeparam>
        /// <param name="selectColumn">查询的列</param>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="cmdParms">参数值</param>
        /// <returns>分页结果</returns>
        IPageResult<TModel> QueryPageList<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null);

        /// <summary>
        /// 异步分页查询（目前只支持MSSQLServer）
        /// </summary>
        /// <typeparam name="TModel">分页结果类型</typeparam>
        /// <param name="selectColumn">查询的列</param>
        /// <param name="selectTable">查询的表</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndex">当前页面</param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="cmdParms">参数值</param>
        /// <returns>分页结果</returns>
        Task<IPageResult<TModel>> QueryPageListAsync<TModel>(string selectColumn, string selectTable, string where, string order, int pageIndex, int pageSize, object cmdParms = null);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        int Update(object data, object condition, string[] ignoreFields = null);

        /// <summary>
        /// 异步更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        Task<int> UpdateAsync(object data, object condition, string[] ignoreFields = null);

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        int Update(object data, Expression<Func<T, bool>> condition, string[] ignoreFields = null);

        /// <summary>
        /// 异步更新信息
        /// </summary>
        /// <param name="data">要更新的信息</param>
        /// <param name="condition">条件</param>
        /// <param name="ignoreFields">忽略的字段</param>
        /// <returns>受影响行数</returns>
        Task<int> UpdateAsync(object data, Expression<Func<T, bool>> condition, string[] ignoreFields = null);
    }
}