using JQCore.Extensions;
using JQCore.Utils;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace JQCore.Redis.StackExchangeRedis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：ConnectionMultiplexerFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/7/15 15:36:22
    /// </summary>
    internal sealed class ConnectionMultiplexerFactory
    {
        private static ConcurrentDictionary<string, ConnectionMultiplexer> _connectionMultiplexerCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();

        private ConnectionMultiplexerFactory()
        {
        }

        /// <summary>
        /// 获取一个连接
        /// </summary>
        /// <param name="connection">连接字符信息</param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnection(string connection)
        {
            if (!_connectionMultiplexerCache.ContainsKey(connection))
            {
                lock (_connectionMultiplexerCache)
                {
                    if (!_connectionMultiplexerCache.ContainsKey(connection))
                    {
                        _connectionMultiplexerCache[connection] = CreateConnection(connection);
                    }
                }
            }
            return _connectionMultiplexerCache[connection];
        }

        /// <summary>
        /// 创建一个连接
        /// </summary>
        /// <param name="connection">连接字符信息</param>
        /// <returns></returns>
        private static ConnectionMultiplexer CreateConnection(string connection)
        {
            var conn = ConnectionMultiplexer.Connect(connection);
            conn.ConnectionFailed += Conn_ConnectionFailed;
            conn.ConnectionRestored += Conn_ConnectionRestored;
            conn.ErrorMessage += Conn_ErrorMessage;

            return conn;
        }

        /// <summary>
        /// redis内部发生错误时回掉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Conn_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            LogUtil.Debug($"redis内部发生错误,{e.EndPoint}:{e.Message}", loggerName: "ConnectionMultiplexerFactory");
        }

        /// <summary>
        /// 重新连接失败时回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Conn_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            LogUtil.Error($"redis重新连接时发生错误,{e.EndPoint}{e.Exception.ToErrMsg()}", loggerName: "ConnectionMultiplexerFactory");
        }

        /// <summary>
        /// 连接失败时回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Conn_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            LogUtil.Error($"redis连接时发生错误,{e.EndPoint}{e.Exception.ToErrMsg()}", loggerName: "ConnectionMultiplexerFactory");
        }

        /// <summary>
        /// 释放全部连接
        /// </summary>
        public static void DisposeConn()
        {
            lock (_connectionMultiplexerCache)
            {
                foreach (var item in _connectionMultiplexerCache)
                {
                    item.Value?.Close();
                    item.Value?.Dispose();
                }
                LogUtil.Info("执行释放全部redis连接");
                _connectionMultiplexerCache.Clear();
            }
        }
    }
}