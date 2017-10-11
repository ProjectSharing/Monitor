namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ICacheOptionProvider.cs
    /// 接口属性：公共
    /// 类功能描述：ICacheOptionProvider接口
    /// 创建标识：yjq 2017/10/2 22:26:40
    /// </summary>
    public interface ICacheOptionProvider
    {
        /// <summary>
        /// 获取缓存配置
        /// </summary>
        RedisCacheOption RedisCacheOption { get; }
    }
}