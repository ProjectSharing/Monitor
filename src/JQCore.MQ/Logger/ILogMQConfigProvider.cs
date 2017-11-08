namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ILogMQConfigProvider.cs
    /// 接口属性：公共
    /// 类功能描述：ILogMQConfigProvider接口
    /// 创建标识：yjq 2017/11/6 20:56:41
    /// </summary>
    public interface ILogMQConfigProvider
    {
        /// <summary>
        /// 获取MQ的配置信息
        /// </summary>
        /// <returns></returns>
        MQConfig GetConfig();
    }
}