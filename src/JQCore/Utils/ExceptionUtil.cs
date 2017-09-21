using System;
using System.Threading.Tasks;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ExceptionUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：异常帮助类
    /// 创建标识：yjq 2017/9/4 17:50:38
    /// </summary>
    public static class ExceptionUtil
    {
        /// <summary>
        /// 吃掉异常
        /// </summary>
        /// <param name="action">执行的方法</param>
        public static void EatException(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }

        /// <summary>
        /// 吃掉异常
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static async Task EatExceptionAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch { }
        }

        /// <summary>
        /// 吃掉异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static T EatException<T>(Func<T> action, T defaultValue = default(T))
        {
            try
            {
                return action();
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 吃掉异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static async Task<T> EatExceptionAsync<T>(Func<Task<T>> action, T defaultValue = default(T))
        {
            try
            {
                return await action();
            }
            catch
            {
                return defaultValue;
            }
        }

        #region 忽略异常，但记录异常

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        public static void LogException(Action action, string memberName = null, string loggerName = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName, loggerName: loggerName);
            }
        }

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <param name="action">执行的方法</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        /// <returns>可等待</returns>
        public static async Task LogExceptionAsync(Func<Task> action, string memberName = null, string loggerName = null)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName, loggerName: loggerName);
            }
        }

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        /// <param name="loggerType">记录器类型</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static T LogException<T>(Func<T> action, T defaultValue = default(T), string memberName = null, string loggerName = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName, loggerName: loggerName);
                return defaultValue;
            }
        }

        /// <summary>
        /// 忽略异常，但记录异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="action">执行的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="memberName">调用成员信息</param>
        /// <param name="loggerName">记录器名字</param>
        /// <returns>如果没异常，返回值就是正常返回值，假如出现了异常，返回值就是默认的值</returns>
        public static async Task<T> LogExceptionAsync<T>(Func<Task<T>> action, T defaultValue = default(T), string memberName = null, string loggerName = null)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName, loggerName: loggerName);
                return defaultValue;
            }
        }

        #endregion 忽略异常，但记录异常
    }
}