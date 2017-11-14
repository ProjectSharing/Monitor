using JQCore.Extensions;
using JQCore.MQ.Serialization;
using JQCore.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;

namespace JQCore.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMQClient.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/31 20:18:07
    /// </summary>
    public sealed class RabbitMQClient : SelfDisposable, IMQClient
    {
        private readonly IMQBinarySerializer _binarySerializer;
        private readonly MQConfig _mqConfig;
        private IModel _channel;

        public RabbitMQClient(MQConfig mqConfig, IMQBinarySerializer binarySerializer)
        {
            mqConfig.NotNull("配置信息不能为空");
            binarySerializer.NotNull("对象序列化接口不能为空");
            _mqConfig = mqConfig;
            _binarySerializer = binarySerializer;
        }

        /// <summary>
        /// 获取通道
        /// </summary>
        private IModel Channel
        {
            get
            {
                return _channel ?? (_channel = RabbitMQConnectionFactory.GetConn(_mqConfig).CreateModel());
            }
        }

        /// <summary>
        /// 声明一个交换机
        /// </summary>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="exchangType">交换机类型
        /// Fanout Exchange – 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都会被转发到与该交换机绑定的所有队列上。很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout交换机转发消息是最快的。
        /// Direct Exchange：处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全匹配。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，则只有被标记为“dog”的消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog。
        /// Topic Exchange – 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。符号“#”匹配一个或多个词，符号“*”匹配不多不少一个词。因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*” 只会匹配到“audit.irs”。
        /// </param>
        /// <param name="durable">是否持久化</param>
        /// <param name="aotuDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        private void ExchangeDeclaare(string exchangeName, string exchangType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            if (exchangeName.IsNotNullAndNotEmptyWhiteSpace())
            {
                Channel.ExchangeDeclare(exchangeName.Trim(), exchangType, durable, autoDelete, arguments);
            }
        }

        /// <summary>
        /// 声明一个队列
        /// </summary>
        /// <param name="queueName">队列名字</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="exclusive">是否为排它队列</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        private void QueueDeclare(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            if (queueName.IsNotNullAndNotEmptyWhiteSpace())
            {
                Channel.QueueDeclare(queueName.Trim(), durable, exclusive, autoDelete, arguments);
            }
        }

        /// <summary>
        /// 将一个队列绑定到交换机上，也可以说是订阅某个关键词
        /// </summary>
        /// <param name="queueName">队列名字</param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="routingKey">路由关键字信息</param>
        /// <param name="arguments">参数</param>
        private void BindQueue(string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments = null)
        {
            if (StringUtil.AllIsNotNullAndNotWhiteSpace(queueName, exchangeName, routingKey))
            {
                Channel.QueueBind(queueName.Trim(), exchangeName.Trim(), routingKey.Trim(), arguments);
            }
        }

        /// <summary>
        /// 声明一个交换机、队列，并绑定对应关系
        /// </summary>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名字</param>
        /// <param name="routingKey">路由关键字信息</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="exclusive">是否为排它队列</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="arguments">参数</param>
        private void CreateExchangeAndQueue(string exchangeName, string queueName, string routingKey, string exchangeType = ExchangeType.Fanout, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null, bool isConsumer = false)
        {
            ExchangeDeclaare(exchangeName, exchangeType, durable, autoDelete, arguments);
            QueueDeclare(queueName, durable, exclusive, autoDelete, arguments);
            BindQueue(queueName, exchangeName, routingKey, arguments);
            if (isConsumer)
            {
                Channel.BasicQos(0, 1, false);
            }
        }

        /// <summary>
        /// 声明一个交换机、队列，并绑定对应关系
        /// </summary>
        /// <param name="mqAttribute">消息队列特性信息</param>
        private void CreateExchangeAndQueue(MQAttribute mqAttribute, bool isConsumer = false)
        {
            mqAttribute.NotNull("MQAttribute不能为空");
            CreateExchangeAndQueue(mqAttribute.ExchangeName, mqAttribute.QueueName, mqAttribute.RoutingKey, exchangeType: mqAttribute.ExchangeType, durable: mqAttribute.Durable, exclusive: mqAttribute.Exclusive, autoDelete: mqAttribute.AutoDelete, arguments: mqAttribute.Arguments, isConsumer: isConsumer);
        }

        #region 发送消息

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="routingKey">路由关键字</param>
        public void Publish<T>(T command, string routingKey = null)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            routingKey.IsNotNullAndNotWhiteSpaceThenExcute(() => mqAttribute.RoutingKey = routingKey);
            CreateExchangeAndQueue(mqAttribute);
            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;
            Channel.BasicPublish(mqAttribute.ExchangeName, mqAttribute.RoutingKey, properties, _binarySerializer.Serialize(command));
        }

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
        public void Publish<T>(T command, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null)
        {
            CreateExchangeAndQueue(exchangeName, queueName, routingKey, exchangeType: exchangeType, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: arguments);
            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;
            Channel.BasicPublish(exchangeName, routingKey, properties, _binarySerializer.Serialize(command));
        }

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
        public void Subscribe<T>(Action<T> actionHandle, Action<T, Exception> errorActionHandle = null, string routingKey = null, string memberName = null, string loggerName = null)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            routingKey.IsNotNullAndNotWhiteSpaceThenExcute(() => mqAttribute.RoutingKey = routingKey);
            CreateExchangeAndQueue(mqAttribute, isConsumer: true);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = _binarySerializer.Deserialize<T>(body);
                try
                {
                    actionHandle(message);
                }
                catch (Exception ex)
                {
                    if (errorActionHandle != null)
                    {
                        ExceptionUtil.LogException(() => { errorActionHandle(message, ex); }, memberName: memberName, loggerName: loggerName);
                    }
                }
                finally
                {
                    Channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            Channel.BasicConsume(mqAttribute.QueueName, false, consumer);
        }

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
        public void Subscribe<T>(Action<T> actionHandle, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, Action<T, Exception> errorActionHandle = null, string memberName = null, string loggerName = null)
        {
            CreateExchangeAndQueue(exchangeName, queueName, routingKey, exchangeType: exchangeType, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: arguments, isConsumer: true);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = _binarySerializer.Deserialize<T>(body);
                try
                {
                    actionHandle(message);
                }
                catch (Exception ex)
                {
                    if (errorActionHandle != null)
                    {
                        ExceptionUtil.LogException(() => { errorActionHandle(message, ex); }, memberName: memberName, loggerName: loggerName);
                    }
                }
                finally
                {
                    Channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            Channel.BasicConsume(queueName, false, consumer);
        }

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
        public void Pull<T>(Action<T> actionHandle, Action<T, Exception> errorActionHandle = null, string routingKey = null, string memberName = null, string loggerName = null)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            routingKey.IsNotNullAndNotWhiteSpaceThenExcute(() => mqAttribute.RoutingKey = routingKey);
            CreateExchangeAndQueue(mqAttribute, isConsumer: true);
            var result = Channel.BasicGet(mqAttribute.QueueName, false);
            if (result.IsNull())
            {
                return;
            }
            var message = _binarySerializer.Deserialize<T>(result.Body);
            try
            {
                actionHandle(message);
            }
            catch (Exception ex)
            {
                if (errorActionHandle != null)
                {
                    ExceptionUtil.LogException(() => { errorActionHandle(message, ex); }, memberName: memberName, loggerName: loggerName);
                }
            }
            finally
            {
                Channel.BasicAck(result.DeliveryTag, false);
            }
        }

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
        public void Pull<T>(Action<T> actionHandle, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, Action<T, Exception> errorActionHandle = null, string memberName = null, string loggerName = null)
        {
            CreateExchangeAndQueue(exchangeName, queueName, routingKey, exchangeType: exchangeType, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: arguments);
            var result = Channel.BasicGet(queueName, false);
            if (result.IsNull())
            {
                return;
            }
            var message = _binarySerializer.Deserialize<T>(result.Body);
            try
            {
                actionHandle(message);
            }
            catch (Exception ex)
            {
                if (errorActionHandle != null)
                {
                    ExceptionUtil.LogException(() => { errorActionHandle(message, ex); }, memberName: memberName, loggerName: loggerName);
                }
            }
            finally
            {
                Channel.BasicAck(result.DeliveryTag, false);
            }
        }

        #endregion 拉取消息

        #region RPC客户端

        /// <summary>
        /// RPC客户端
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="command">消息内容</param>
        /// <param name="routingKey">路由关键字</param>
        /// <returns>处理之后的信息</returns>
        public T RpcClient<T>(T command, string routingKey = null)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            routingKey.IsNotNullAndNotWhiteSpaceThenExcute(() => mqAttribute.RoutingKey = routingKey);
            mqAttribute.NotNull("MQAttribute不能为空");
            return RpcClient(command, mqAttribute.ExchangeName, mqAttribute.QueueName, mqAttribute.RoutingKey
                 , exchangeType: mqAttribute.ExchangeType, durable: mqAttribute.Durable, autoDelete: mqAttribute.AutoDelete, exclusive: mqAttribute.Exclusive, arguments: mqAttribute.Arguments);
        }

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
        public T RpcClient<T>(T command, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null)
        {
            throw new NotSupportedException();
            //CreateExchangeAndQueue(exchangeName, queueName, routingKey, exchangeType: exchangeType, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: arguments);
            //var consumer = new EventingBasicConsumer(Channel);
            //Channel.BasicConsume(queueName, true, consumer);

            //try
            //{
            //    var correlationId = Guid.NewGuid().ToString();
            //    var basicProperties = Channel.CreateBasicProperties();
            //    basicProperties.ReplyTo = queueName;
            //    basicProperties.CorrelationId = correlationId;

            //    Channel.BasicPublish(exchangeName, routingKey, basicProperties, _binarySerializer.Serialize(command));

            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var response = Encoding.UTF8.GetString(body);
            //        if (ea.BasicProperties.CorrelationId == correlationId)
            //        {
            //            var result = _binarySerializer.Deserialize<T>(ea.Body);
            //        }
            //    };

            //    //var sw = Stopwatch.StartNew();
            //    //while (true)
            //    //{
            //    //    var ea = consumer.Queue.Dequeue();
            //    //    if (ea.BasicProperties.CorrelationId == correlationId)
            //    //    {
            //    //        return _binarySerializer.Deserialize<T>(ea.Body);
            //    //    }

            //    //    if (sw.ElapsedMilliseconds > 30000)
            //    //    {
            //    //        sw.Stop();
            //    //        throw new Exception("等待响应超时");
            //    //    }
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

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
        public void RpcServer<T>(Func<T, T> handler, string routingKey = null, string memberName = null, string loggerName = null)
        {
            var mqAttribute = MQAttribute.GetMQAttribute<T>();
            mqAttribute.NotNull("MQAttribute不能为空");
            routingKey.IsNotNullAndNotWhiteSpaceThenExcute(() => mqAttribute.RoutingKey = routingKey);
            RpcServer(handler, mqAttribute.ExchangeName, mqAttribute.QueueName, mqAttribute.RoutingKey
                , exchangeType: mqAttribute.ExchangeType, durable: mqAttribute.Durable, autoDelete: mqAttribute.AutoDelete, exclusive: mqAttribute.Exclusive, arguments: mqAttribute.Arguments, memberName: memberName, loggerName: loggerName);
        }

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
        public void RpcServer<T>(Func<T, T> handler, string exchangeName, string queueName, string routingKey, string exchangeType = MQExchangeType.FANOUT, bool durable = true, bool autoDelete = false, bool exclusive = false, IDictionary<string, object> arguments = null, string memberName = null, string loggerName = null)
        {
            CreateExchangeAndQueue(exchangeName, queueName, routingKey, exchangeType: exchangeType, durable: durable, exclusive: exclusive, autoDelete: autoDelete, arguments: arguments);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = _binarySerializer.Deserialize<T>(ea.Body);

                var props = ea.BasicProperties;
                var replyProps = Channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;
                message = ExceptionUtil.LogException(() =>
                {
                    return handler(message);
                }, memberName: memberName, loggerName: loggerName);
                Channel.BasicPublish(exchangeName, props.ReplyTo, replyProps, _binarySerializer.Serialize(message));
                Channel.BasicAck(ea.DeliveryTag, false);
            };
            Channel.BasicConsume(queueName, false, consumer);
        }

        #endregion RPC服务端

        protected override void DisposeCode()
        {
            _channel?.Close();
            _channel?.Dispose();
        }
    }
}