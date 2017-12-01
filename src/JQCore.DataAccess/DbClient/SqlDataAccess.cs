using Dapper;
using JQCore.DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JQCore.DataAccess.DbClient
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlDataAccess.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/5 20:09:31
    /// </summary>
    public sealed partial class SqlDataAccess : BaseDataAccess, IDataAccess
    {
        #region .ctor

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dbProperty">数据库属性信息</param>
        /// <param name="isWrite">是否写连接（默认使用读连接）</param>
        public SqlDataAccess(DatabaseProperty dbProperty, bool isWrite = false)
            : base(dbProperty, isWrite)
        {
        }

        #endregion .ctor

        #region 执行增删改方法，返回受影响的行数

        /// <summary>
        /// 执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Execute(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 异步执行增删改方法，返回受影响的行数
        /// </summary>
        /// <param name="query">请求内容</param>
        /// <returns>受影响的行数</returns>
        public async Task<int> ExecuteNonQueryAsync(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.ExecuteAsync(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        #endregion 执行增删改方法，返回受影响的行数

        #region 取出第一条数据

        /// <summary>
        /// 取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        public T ExecuteScalar<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.ExecuteScalar<T>(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 异步取出第一条数据
        /// </summary>
        /// <typeparam name="T">数据返回类型</typeparam>
        /// <param name="query">请求内容</param>
        /// <returns>单条数据内容</returns>
        public async Task<T> ExecuteScalarAsync<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.ExecuteScalarAsync<T>(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        #endregion 取出第一条数据

        #region 查询

        #region 返回第一条结果信息

        /// <summary>
        /// 返回第一条结果信息
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>第一条结果信息</returns>
        public T QuerySingleOrDefault<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.QueryFirstOrDefault<T>(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 返回第一条结果信息
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>第一条结果信息</returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryFirstOrDefaultAsync<T>(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        #endregion 返回第一条结果信息

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>结果集</returns>
        public IEnumerable<T> Query<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Query<T>(query.CommandText, query.ParaItems, Tran, true, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <returns>结果集</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(SqlQuery query)
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryAsync<T>(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(SqlQuery query, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Query(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="map">匹配关系</param>
        /// <param name="splitOn">分割字段</param>
        /// <returns>结果集</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(SqlQuery query, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryAsync(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Query(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryAsync(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Query(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryAsync(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                return _conn.Query(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

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
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(SqlQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id")
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                return await _conn.QueryAsync(query.CommandText,
                               map: map,
                               param: query.ParaItems,
                               transaction: Tran,
                               splitOn: splitOn,
                               commandTimeout: query.CommandTimeout,
                               commandType: query.CommandType);
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 返回多个结果
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="action">将GridReader转为查询结果的方法</param>
        /// <returns>多个结果</returns>
        public TResult QueryMultiple<TResult>(SqlQuery query, Func<SqlMapper.GridReader, TResult> action)
        {
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                using (var multiple = _conn.QueryMultiple(query.CommandText, param: query.ParaItems, transaction: Tran, commandTimeout: query.CommandTimeout, commandType: query.CommandType))
                {
                    return action(multiple);
                }
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 异步返回多个结果
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        /// <param name="query">查询请求内容</param>
        /// <param name="action">将GridReader转为查询结果的方法</param>
        /// <returns>多个结果</returns>
        public async Task<TResult> QueryMultipleAsync<TResult>(SqlQuery query, Func<SqlMapper.GridReader, TResult> action)
        {
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.MonitorAsync(query, async () =>
            {
                using (var multiple = await _conn.QueryMultipleAsync(query.CommandText, param: query.ParaItems, transaction: Tran, commandTimeout: query.CommandTimeout, commandType: query.CommandType))
                {
                    return action(multiple);
                }
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 查询（Table）
        /// </summary>
        /// <param name="query">查询请求内容</param>
        /// <returns>Table查询结果</returns>
        public DataTable QueryTable(SqlQuery query)
        {
            DataTable table = new DataTable();
            Open();
            PrepareSqlQuery(query);
            return SqlMonitorUtil.Monitor(query, () =>
            {
                using (IDataReader reader = _conn.ExecuteReader(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType))
                {
                    table.Load(reader);
                    return table;
                }
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        /// <summary>
        /// 异步查询（Table）
        /// </summary>
        /// <param name="query">查询请求内容</param>
        /// <returns>Table查询结果</returns>
        public async Task<DataTable> QueryTableAsync(SqlQuery query)
        {
            DataTable table = new DataTable();
            Open();
            PrepareSqlQuery(query);
            return await SqlMonitorUtil.Monitor(query, async () =>
            {
                using (IDataReader reader = await _conn.ExecuteReaderAsync(query.CommandText, query.ParaItems, Tran, query.CommandTimeout, query.CommandType))
                {
                    table.Load(reader);
                    return table;
                }
            }, dbType: DatabaseTyoe.ToString(), dbConnection: Connection);
        }

        #endregion 查询
    }
}