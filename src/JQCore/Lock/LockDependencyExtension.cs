using Autofac;
using JQCore.Dependency;

namespace JQCore.Lock
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LockDependencyExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：LockDependcyExtension
    /// 创建标识：yjq 2017/9/21 22:43:41
    /// </summary>
    public static class LockDependencyExtension
    {
        /// <summary>
        /// 使用默认的字节数组序列化
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder UseLocalLock(this ContainerBuilder builder)
        {
            builder.AddSingleton<ILock, LocalLock>();
            return builder;
        }
    }
}