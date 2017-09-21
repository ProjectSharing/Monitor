using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JQCore.DataAccess.DbClient
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IDataAccess.cs
    /// 接口属性：公共
    /// 类功能描述：IDataAccess接口
    /// 创建标识：yjq 2017/9/5 20:08:25
    /// </summary>
    public interface IDataAccess : IDisposable
    {
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 超时时间
        /// </summary>
        int CommandTimeOut { get; }

        /// <summary>
        /// 开始事务操作
        /// </summary>
        /// <returns>事务</returns>
        IDbTransaction BeginTran();

        /// <summary>
        /// 开始事务操作
        /// </summary>
        /// <param name="il">事务隔离级别</param>
        /// <returns>事务</returns>
        IDbTransaction BeginTran(IsolationLevel il);

        /// <summary>
        /// 打开连接
        /// </summary>
        void Open();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTran();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(SqlQuery query);

        /// <summary>
        /// 异步执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteNonQueryAsync(SqlQuery query);

        /// <summary>
        /// 取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        T ExecuteScalar<T>(SqlQuery query);

        /// <summary>
        /// 异步取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        Task<T> ExecuteScalarAsync<T>(SqlQuery query);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>结果集</returns>
        IEnumerable<T> Query<T>(SqlQuery query);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">The Field we should split and read the second object from (default: id)</param>
        /// <returns>结果集</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(SqlQuery query, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TFifth"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<T>> QueryAsync<T>(SqlQuery query);

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">The Field we should split and read the second object from (default: id)</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(SqlQuery query, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 异步查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <typeparam name="TFourth"></typeparam>
        /// <typeparam name="TFifth"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id");

        /// <summary>
        /// 返回多个结果
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="action">将GridReader转为查询结果的方法</param>
        /// <returns>多个结果</returns>
        TResult QueryMultiple<TResult>(SqlQuery query, Func<SqlMapper.GridReader, TResult> action);

        /// <summary>
        /// 异步返回多个结果
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="action">将GridReader转为查询结果的方法</param>
        /// <returns>多个结果</returns>
        Task<TResult> QueryMultipleAsync<TResult>(SqlQuery query, Func<SqlMapper.GridReader, TResult> action);

        /// <summary>
        /// 返回第一条结果信息
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>第一条结果信息</returns>
        T QuerySingleOrDefault<T>(SqlQuery query);

        /// <summary>
        /// 异步返回第一条结果信息
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>第一条结果信息</returns>
        Task<T> QuerySingleOrDefaultAsync<T>(SqlQuery query);

        /// <summary>
        /// 查询（Table）
        /// </summary>
        /// <param name="query">查询请求内容</param>
        /// <returns>Table查询结果</returns>
        DataTable QueryTable(SqlQuery query);

        /// <summary>
        /// 异步查询（Table）
        /// </summary>
        /// <param name="query">查询请求内容</param>
        /// <returns>Table查询结果</returns>
        Task<DataTable> QueryTableAsync(SqlQuery query);

        /// <summary>
        /// 批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">列表信息</param>
        /// <param name="tableName">表名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        void BulkInsert<T>(List<T> list, string tableName, string[] ignoreFields = null);

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="list">列表信息</param>
        /// <param name="tableName">表名字</param>
        /// <param name="ignoreFields">要忽略的字段</param>
        /// <returns>Task</returns>
        Task BulkInsertAsync<T>(List<T> list, string tableName, string[] ignoreFields = null);

        /// <summary>
        /// 批量信息(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="tableName">数据库名字</param>
        void BulkInsert(DataTable table, string tableName);

        /// <summary>
        /// 异步批量插入(目前只支持MSSQLServer)
        /// </summary>
        /// <param name="table">table</param>
        /// <param name="tableName">表名字</param>
        /// <returns>Task</returns>
        Task BulkInsertAsync(DataTable table, string tableName);
    }
}