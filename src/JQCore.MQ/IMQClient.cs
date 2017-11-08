using System;
using System.Collections.Generic;

namespace JQCore.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IMQClient.cs
    /// 接口属性：公共
    /// 类功能描述：IMQClient接口
    /// 创建标识：yjq 2017/10/31 20:16:51
    /// </summary>
    public interface IMQClient : IDisposable
    {
        #region 发送消息

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="routingKey">路由关键字</param>
        void Publish<T>(T command, string routingKey = null);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exclusive">是否申明为排它队列</param>
        /// <param name="arguments">参数</param>
        void Publish<T>(T command, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null);

        #endregion 发送消息

        #region 订阅消息

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="actionHandle">处理该类型消息的方法</param>
        /// <param name="errorActionHandle">处理消息时发生错误时处理方法</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void Subscribe<T>(Action<T> actionHandle, Action<T, Exception> errorActionHandle = null, string routingKey = null, string memberName = null, string loggerName = null);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="actionHandle">处理该类型消息的方法</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exclusive">是否申明为排它队列</param>
        /// <param name="arguments">参数</param>
        /// <param name="errorActionHandle">处理消息时发生错误时处理方法</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void Subscribe<T>(Action<T> actionHandle, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, Action<T, Exception> errorActionHandle = null, string memberName = null, string loggerName = null);

        #endregion 订阅消息

        #region 拉取消息

        /// <summary>
        /// 拉取消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="actionHandle">处理该类型消息的方法</param>
        /// <param name="errorActionHandle">处理消息时发生错误时处理方法</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void Pull<T>(Action<T> actionHandle, Action<T, Exception> errorActionHandle = null, string routingKey = null, string memberName = null, string loggerName = null);

        /// <summary>
        /// 拉取消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="actionHandle">处理该类型消息的方法</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exclusive">是否申明为排它队列</param>
        /// <param name="arguments">参数</param>
        /// <param name="errorActionHandle">处理消息时发生错误时处理方法</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void Pull<T>(Action<T> actionHandle, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, Action<T, Exception> errorActionHandle = null, string memberName = null, string loggerName = null);

        #endregion 拉取消息

        #region RPC客户端

        /// <summary>
        /// RPC客户端
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="routingKey">路由关键字</param>
        /// <returns>处理之后的信息</returns>
        T RpcClient<T>(T command, string routingKey = null);

        /// <summary>
        /// RPC客户端
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exclusive">是否申明为排它队列</param>
        /// <param name="arguments">参数</param>
        /// <returns>处理之后的信息</returns>
        T RpcClient<T>(T command, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null);

        #endregion RPC客户端

        #region RPC服务端

        /// <summary>
        /// RPC服务端
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">处理该类型消息的方法</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void RpcServer<T>(Func<T, T> handler, string routingKey = null, string memberName = null, string loggerName = null);

        /// <summary>
        /// RPC服务端
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="handler">处理该类型消息的方法</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名</param>
        /// <param name="routingKey">路由关键字</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exclusive">是否申明为排它队列</param>
        /// <param name="arguments">参数</param>
        /// <param name="errorActionHandle">处理消息时发生错误时处理方法</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        void RpcServer<T>(Func<T, T> handler, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, string memberName = null, string loggerName = null);

        #endregion RPC服务端
    }
}