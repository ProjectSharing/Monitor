using JQCore.Dependency;
using JQCore.MQ.Logger;
using JQCore.Utils;
using System.Collections.Generic;

namespace JQCore.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseMonitorSendUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/30 2:53:46
    /// </summary>
    public static class MonitorSendUtil
    {
        /// <summary>
        /// 获取MQLoggerConfig的服务器配置
        /// </summary>
        /// <returns></returns>
        private static MQConfig GetConfig()
        {
            var logMQConfigProvider = ContainerManager.Resolve<ILogMQConfigProvider>();
            return logMQConfigProvider.GetConfig();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageList"></param>
        /// <param name="exchangeName">交换机名字</param>
        /// <param name="queueName">队列名字</param>
        /// <param name="routeKey">路由</param>
        public static void SendMessage<T>(List<T> messageList, string exchangeName, string queueName, string routeKey)
        {
            if (messageList != null && messageList.Count > 0)
            {
                ExceptionUtil.LogException(() =>
                {
                    var conifg = GetConfig();
                    using (var mqClient = ContainerManager.Resolve<IMQFactory>().Create(conifg))
                    {
                        mqClient.Publish(messageList, exchangeName, queueName, routeKey, exchangeType: MQExchangeType.TOPICS, durable: true);
                    }
                }, memberName: "MessageSendUtil-MessageHandle-SendMessag");
            }
        }
    }
}