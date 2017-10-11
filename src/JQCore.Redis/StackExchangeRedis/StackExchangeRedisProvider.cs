using JQCore.Redis.Serialization;

namespace JQCore.Redis.StackExchangeRedis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：StackExchangeRedisProvider.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/7/15 15:54:37
    /// </summary>
    public sealed class StackExchangeRedisProvider : IRedisDatabaseProvider
    {
        public StackExchangeRedisProvider()
        {
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOption redisCacheOption)
        {
            return new StackExchangeRedisClient(redisCacheOption, new RedisJsonBinarySerializer());
        }

        /// <summary>
        /// 创建redis客户端
        /// </summary>
        /// <param name="redisCacheOption">redis配置信息</param>
        /// <param name="serializer">序列化类</param>
        /// <returns></returns>
        public IRedisClient CreateClient(RedisCacheOption redisCacheOption, IRedisBinarySerializer serializer)
        {
            return new StackExchangeRedisClient(redisCacheOption, serializer);
        }
    }
}