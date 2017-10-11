using System;
using System.Threading.Tasks;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisClientExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：redis实例接口扩展类
    /// 创建标识：yjq 2017/7/15 20:39:40
    /// </summary>
    public static class RedisClientExtension
    {
        /// <summary>
        /// 设置值并设置过期时间
        /// </summary>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="setAction">设置值的方法</param>
        public static void SetAndSetExpireTime(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Action<IRedisClient, string> setAction)
        {
            setAction(redisClient, key);
            redisClient.Expire(key, expireTimeSpan);
        }

        /// <summary>
        /// 异步设置值并设置过期时间
        /// </summary>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="setAction">设置值的方法</param>
        /// <returns>结果可等待</returns>
        public async static Task SetAndSetExpireTimeAsync(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Func<IRedisClient, string, Task> setAction)
        {
            await setAction(redisClient, key);
            await redisClient.ExpireAsync(key, expireTimeSpan);
            return;
        }

        /// <summary>
        /// 设置值并设置过期时间
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="setAction">设置值的方法</param>
        /// <returns>值</returns>
        public static T SetAndSetExpireTime<T>(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Func<IRedisClient, string, T> setAction)
        {
            var result = setAction(redisClient, key);
            redisClient.Expire(key, expireTimeSpan);
            return result;
        }

        /// <summary>
        /// 异步设置值并设置过期时间
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="setAction">设置值的方法</param>
        /// <returns>值</returns>
        public async static Task<T> SetAndSetExpireTimeAsync<T>(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Func<IRedisClient, string, Task<T>> setAction)
        {
            var result = await setAction(redisClient, key);
            await redisClient.ExpireAsync(key, expireTimeSpan);
            return result;
        }

        /// <summary>
        /// 获取并设置过期时间
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="getAction">获取的方法</param>
        /// <returns>缓存值</returns>
        public static T GetAndSetExpireTime<T>(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Func<IRedisClient, string, T> getAction)
        {
            var result = getAction(redisClient, key);
            redisClient.Expire(key, expireTimeSpan);
            return result;
        }

        /// <summary>
        /// 异步获取并设置过期时间
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="redisClient">redis实例接口</param>
        /// <param name="key">键</param>
        /// <param name="expireTimeSpan">过期时间</param>
        /// <param name="getAction">获取的方法</param>
        /// <returns>缓存值</returns>
        public async static Task<T> GetAndSetExpireTimeAsync<T>(this IRedisClient redisClient, string key, TimeSpan expireTimeSpan, Func<IRedisClient, string, Task<T>> getAction)
        {
            var result = await getAction(redisClient, key);
            await redisClient.ExpireAsync(key, expireTimeSpan);
            return result;
        }
    }
}