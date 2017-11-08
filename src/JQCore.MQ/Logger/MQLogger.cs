using JQCore.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace JQCore.MQ.Logger
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：MQLogger.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/11/7 20:36:58
    /// </summary>
    public class MQLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return new ScopeDispose();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var message = formatter(state, exception);
            var projectName = ConfigurationManage.GetValue($"MQMonitor:ProjectInfo:ProjectName");
            var source = ConfigurationManage.GetValue<int>($"MQMonitor:ProjectInfo:FSource");
            var messageInfo = MessageInfo.Create(message, ConvertLogLevel(logLevel), projectName, source);
            MessageSendUtil._MessageQueue.EnqueueMessage(messageInfo); ;
        }

        /// <summary>
        /// 转化消息类型
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        private static MessageType ConvertLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    return MessageType.Debug;

                case LogLevel.Information:
                    return MessageType.Info;

                case LogLevel.Warning:
                    return MessageType.Warn;

                case LogLevel.Error:
                    return MessageType.Error;

                case LogLevel.Critical:
                    return MessageType.Fatal;

                case LogLevel.None:
                    return MessageType.Debug;

                default:
                    return MessageType.Debug;
            }
        }

        public class ScopeDispose : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}