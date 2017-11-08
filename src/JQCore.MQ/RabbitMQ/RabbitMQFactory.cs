using JQCore.MQ.Serialization;

namespace JQCore.MQ.RabbitMQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RabbitMQFactory.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/31 20:53:12
    /// </summary>
    public sealed class RabbitMQFactory : IMQFactory
    {
        private readonly IMQBinarySerializer _binarySerializer;

        public RabbitMQFactory(IMQBinarySerializer binarySerializer)
        {
            _binarySerializer = binarySerializer;
        }

        /// <summary>
        /// 创建一个RabbitMq客户端
        /// </summary>
        /// <param name="mqConfig">mq配置信息</param>
        /// <returns>RabbitMq客户端</returns>
        public IMQClient Create(MQConfig mqConfig)
        {
            return new RabbitMQClient(mqConfig, _binarySerializer);
        }
    }
}