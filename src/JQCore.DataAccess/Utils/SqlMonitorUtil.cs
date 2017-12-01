using JQCore.DataAccess.DbClient;
using JQCore.Utils;
using System;
using System.Data;
using System.Threading.Tasks;

namespace JQCore.DataAccess.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SqlMonitorUtil.cs
    /// 类属性：局部类（非静态）
    /// 类功能描述：sql监控帮助类
    /// 创建标识：yjq 2017/9/5 19:47:09
    /// </summary>
    internal static class SqlMonitorUtil
    {
        #region 监控消耗时间

        /// <summary>
        /// sql的日志记录器名字
        /// </summary>
        internal const string _LOGGER_SQL = "JQCore.Public.SqlMonitor";

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <param name="action">执行方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="memberName">调用方法</param>
        /// <param name="dbConnection">数据库连接</param>
        public static void Monitor(Action action, string dbType = null, string memberName = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql方法:{memberName}", _LOGGER_SQL);
                LogUtil.Error(ex);
                isSuccess = false;
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(memberName, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <param name="query">SqlQuery</param>
        /// <param name="action">执行方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbConnection">数据库连接</param>
        public static void Monitor(SqlQuery query, Action action, string dbType = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql语句:{query.CommandText}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(query, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="action">执行方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="memberName">调用方法</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>返回值</returns>
        public static T Monitor<T>(Func<T> action, string dbType = null, string memberName = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql方法:{memberName}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(memberName, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="query">SqlQuery</param>
        /// <param name="action">执行方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>返回值</returns>
        public static T Monitor<T>(SqlQuery query, Func<T> action, string dbType = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql语句:{query.CommandText}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(query, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <param name="action">异步方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="memberName">调用方法</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>任务</returns>
        public async static Task MonitorAsync(Func<Task> action, string dbType = null, string memberName = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql方法:{memberName}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(memberName, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <param name="query">SqlQuery</param>
        /// <param name="action">异步方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>任务</returns>
        public async static Task MonitorAsync(SqlQuery query, Func<Task> action, string dbType = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql语句:{query.CommandText}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(query, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="action">异步方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="memberName">调用方法</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>返回值</returns>
        public async static Task<T> MonitorAsync<T>(Func<Task<T>> action, string dbType = null, string memberName = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql方法:{memberName}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(memberName, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        /// <summary>
        /// 监控消耗时间
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="query">SqlQuery</param>
        /// <param name="action">异步方法</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbConnection">数据库连接</param>
        /// <returns>返回值</returns>
        public async static Task<T> MonitorAsync<T>(SqlQuery query, Func<Task<T>> action, string dbType = null, IDbConnection dbConnection = null)
        {
            var beginTime = DateTimeUtil.Now;
            bool isSuccess = true;
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error($"执行的sql语句:{query.CommandText}", _LOGGER_SQL);
                LogUtil.Error(ex);
                throw;
            }
            finally
            {
                var endTime = DateTimeUtil.Now;
                SqlSendUtil.GrabSql(query, (endTime - beginTime).TotalMilliseconds, isSuccess, dbType: dbType, dataBaseName: dbConnection?.Database);
            }
        }

        #endregion 监控消耗时间
    }
}