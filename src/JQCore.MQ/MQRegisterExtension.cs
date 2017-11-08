using JQCore.Dependency;
using JQCore.MQ.RabbitMQ;
using JQCore.MQ.Serialization;
using Microsoft.AspNetCore.Hosting;

namespace JQCore.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQRegisterExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/31 20:54:19
    /// </summary>
    public static class MQRegisterExtension
    {
        /// <summary>
        /// 使用rabbitMQ
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static ContainerManager UseRabbitMQ(this ContainerManager containerManager)
        {
            return containerManager.AddSingleton<IMQBinarySerializer, MQJsonBinarySerializer>()
                                   .AddSingleton<IMQFactory, RabbitMQFactory>()
                                   ;
        }

        /// <summary>
        /// 注册MQ停止的方法
        /// </summary>
        /// <param name="applicationLifetime"></param>
        /// <returns></returns>
        public static IApplicationLifetime RegisterMQShutDown(this IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(RabbitMQConnectionFactory.DisposeConn);
            return applicationLifetime;
        }
    }
}