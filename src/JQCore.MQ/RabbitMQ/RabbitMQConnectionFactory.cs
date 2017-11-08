using RabbitMQ.Client;
using System.Collections.Concurrent;

namespace JQCore.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMQConnectionFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/31 20:15:00
    /// </summary>
    internal sealed class RabbitMQConnectionFactory
    {
        private RabbitMQConnectionFactory()
        {
        }

        private static ConcurrentDictionary<string, IConnection> _connCache = new ConcurrentDictionary<string, IConnection>();

        /// <summary>
        /// 获取mq连接信息
        /// </summary>
        /// <param name="mqConfig">配置信息</param>
        /// <returns></returns>
        public static IConnection GetConn(MQConfig mqConfig)
        {
            if (!IsHaveCreate(mqConfig))
            {
                lock (_connCache)
                {
                    if (!IsHaveCreate(mqConfig))
                    {
                        _connCache[mqConfig.ToString()] = new ConnectionFactory
                        {
                            HostName = mqConfig.HostName,
                            Password = mqConfig.Password,
                            NetworkRecoveryInterval = mqConfig.NetworkRecoveryInterval,
                            RequestedHeartbeat = mqConfig.RequestedHeartbeat,
                            UserName = mqConfig.UserName,
                            VirtualHost = mqConfig.VirtualHost
                        }.CreateConnection();
                    }
                }
            }
            return _connCache[mqConfig.ToString()];
        }

        private static bool IsHaveCreate(MQConfig config)
        {
            return _connCache.ContainsKey(config.ToString());
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        public static void DisposeConn()
        {
            lock (_connCache)
            {
                foreach (var item in _connCache)
                {
                    item.Value?.Close();
                    item.Value?.Dispose();
                }
                _connCache.Clear();
            }
        }
    }
}