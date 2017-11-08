namespace JQCore.MQ
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IMQFactory.cs
    /// 接口属性：公共
    /// 类功能描述：IMQFactory接口
    /// 创建标识：yjq 2017/10/31 20:17:32
    /// </summary>
    public interface IMQFactory
    {
        /// <summary>
        /// 创建MQ客户端
        /// </summary>
        /// <param name="mqConfig">MQ配置信息</param>
        /// <returns>MQ客户端</returns>
        IMQClient Create(MQConfig mqConfig);
    }
}