using JQCore.Dependency;
using Microsoft.Extensions.Logging;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQLogRegisterExtension.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/8 16:01:13
    /// </summary>
    public static class MQLogRegisterExtension
    {
        /// <summary>
        /// 添加MQLog
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static ILoggerFactory AddMQLog(this ILoggerFactory factory)
        {
            using (var provider = new MQLogProvider())
            {
                factory.AddProvider(provider);
            }
            return factory;
        }

        /// <summary>
        /// 使用MQLog
        /// </summary>
        /// <param name="containerManager"></param>
        /// <returns></returns>
        public static ContainerManager UseMQLog(this ContainerManager containerManager)
        {
            containerManager.AddSingleton<ILogMQConfigProvider, DefaultConfigProvider>();
            return containerManager;
        }
    }
}