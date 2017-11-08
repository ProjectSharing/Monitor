namespace JQCore.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQExchangeType.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：消息队列交换机类型
    /// 创建标识：yjq 2017/10/31 19:45:24
    /// </summary>
    public static class MQExchangeType
    {
        /// <summary>
        /// Exchange type used for AMQP direct exchanges.
        /// </summary>
        public const string DIRECT = "direct";

        /// <summary>
        /// Exchange type used for AMQP fanout exchanges.
        /// </summary>
        public const string FANOUT = "fanout";

        /// <summary>
        /// Exchange type used for AMQP headers exchanges.
        /// </summary>
        public const string HEASERS = "headers";

        /// <summary>
        /// Exchange type used for AMQP topic exchanges.
        /// </summary>
        public const string TOPICS = "topic";
    }
}