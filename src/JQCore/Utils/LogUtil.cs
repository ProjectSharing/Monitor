using JQCore.Dependency;
using JQCore.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LogUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：日志工具类
    /// 创建标识：yjq 2017/9/4 11:41:55
    /// </summary>
    public static class LogUtil
    {
        /// <summary>
        /// 获取ILogger
        /// </summary>
        /// <param name="loggerName">记录器名字</param>
        /// <returns>ILogger</returns>
        private static ILogger GetLogger(string loggerName = null)
        {
            var loggerFactory = ContainerManager.Resolve<ILoggerFactory>();

            if (string.IsNullOrWhiteSpace(loggerName))
            {
                loggerName = "JQCore.Public.RunLog";
            }
            return loggerFactory.CreateLogger(loggerName);
        }

        /// <summary>
        /// 输出追踪日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Trace(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogTrace(msg);
        }

        /// <summary>
        /// 输出调试日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Debug(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogDebug(msg);
        }

        /// <summary>
        /// 输出普通日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Info(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogInformation(msg);
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Warn(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogWarning(msg);
        }

        /// <summary>
        /// 输出警告日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="memberName">调用方法名字</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Warn(Exception ex, string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogWarning(ex.ToErrMsg(memberName: memberName));
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Error(string msg, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogError(msg);
        }

        /// <summary>
        /// 输出错误日志信息
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="loggerName">记录器名字</param>
        public static void Error(Exception ex, string memberName = null, string loggerName = null)
        {
            GetLogger(loggerName: loggerName).LogError(ex.ToErrMsg(memberName: memberName));
        }
    }
}