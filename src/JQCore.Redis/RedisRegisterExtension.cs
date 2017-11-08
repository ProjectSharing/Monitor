using JQCore.Dependency;
using JQCore.Lock;
using JQCore.Redis.Serialization;
using JQCore.Redis.StackExchangeRedis;
using Microsoft.AspNetCore.Hosting;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：RedisRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/3 13:33:08
    /// </summary>
    public static class RedisRegisterExtension
    {
        /// <summary>
        /// 使用redis
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static ContainerManager UseRedis(this ContainerManager containerManager)
        {
            return containerManager.AddScoped<IRedisBinarySerializer, RedisJsonBinarySerializer>()
                              .AddScoped<IRedisDatabaseProvider, StackExchangeRedisProvider>()
                              ;
        }

        /// <summary>
        /// 使用redis分布式锁
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static ContainerManager UseRedisLock(this ContainerManager containerManager)
        {
            return containerManager.AddScoped<ILockOptionProvider, DefaultLockOptionProvider>()
                                   .AddScoped<ILock, DefaultRedisLock>()
                                   ;
        }

        /// <summary>
        /// 注册redis停止的方法
        /// </summary>
        /// <param name="applicationLifetime"></param>
        /// <returns></returns>
        public static IApplicationLifetime RegisterRedisShutDown(this IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(ConnectionMultiplexerFactory.DisposeConn);
            return applicationLifetime;
        }
    }
}