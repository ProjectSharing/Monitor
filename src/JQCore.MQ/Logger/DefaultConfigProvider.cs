namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DefaultConfigProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/6 20:57:37
    /// </summary>
    public class DefaultConfigProvider : ILogMQConfigProvider
    {
        /// <summary>
        /// 获取MQ的配置信息
        /// </summary>
        /// <returns></returns>
        public MQConfig GetConfig()
        {
            return MQConfig.GetConfig("MQMonitor");
        }
    }
}