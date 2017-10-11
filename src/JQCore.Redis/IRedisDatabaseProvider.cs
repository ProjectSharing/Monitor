using JQCore.Redis.Serialization;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IRedisDatabaseProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：IRedisDatabaseProvider接口
    /// 创建标识：yjq 2017/7/15 14:48:33
    /// </summary>
    public interface IRedisDatabaseProvider
    {
        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisCacheOption redisCacheOption);

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        IRedisClient CreateClient(RedisCacheOption redisCacheOption, IRedisBinarySerializer serializer);
    }
}