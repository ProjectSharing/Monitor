using Autofac;
using JQCore.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace JQCore.Serialization
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SerializerServiceExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SerializerServiceExtension
    /// 创建标识：yjq 2017/9/4 22:03:58
    /// </summary>
    public static class SerializerServiceExtension
    {
        /// <summary>
        /// 使用jsonnet
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseJsonNet(this IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            return services;
        }

        /// <summary>
        /// 使用jsonnet
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder UseJsonNet(this ContainerBuilder builder)
        {
            builder.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
            return builder;
        }

        /// <summary>
        /// 使用默认的字节数组序列化
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UseDefaultBinarySerailizer(this IServiceCollection services)
        {
            services.AddSingleton<IBinarySerializer, DefaultBinarySerializer>();
            return services;
        }

        /// <summary>
        /// 使用默认的字节数组序列化
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder UseDefaultBinarySerailizer(this ContainerBuilder builder)
        {
            builder.AddSingleton<IBinarySerializer, DefaultBinarySerializer>();
            return builder;
        }
    }
}