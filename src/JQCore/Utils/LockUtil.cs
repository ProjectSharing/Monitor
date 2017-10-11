using JQCore.Dependency;
using JQCore.Lock;
using System;
using System.Threading.Tasks;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LockUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：LockUtil
    /// 创建标识：yjq 2017/9/21 22:46:18
    /// </summary>
    public static class LockUtil
    {
        private static ILock LockProvider()
        {
            return ContainerManager.Resolve<ILock>();
        }

        /// <summary>
        /// 获取一个锁(需要自己释放)
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <returns>成功返回true</returns>
        public static bool LockTake(string key, string value, TimeSpan span)
        {
            return LockProvider().LockTake(key, value, span);
        }

        /// <summary>
        /// 异步获取一个锁(需要自己释放)
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <returns>成功返回true</returns>
        public static Task<bool> LockTakeAsync(string key, string value, TimeSpan span)
        {
            return LockProvider().LockTakeAsync(key, value, span);
        }

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <returns>成功返回true</returns>
        public static bool LockRelease(string key, string value)
        {
            return LockProvider().LockRelease(key, value);
        }

        /// <summary>
        /// 异步释放一个锁
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <returns>成功返回true</returns>
        public static Task<bool> LockReleaseAsync(string key, string value)
        {
            return LockProvider().LockReleaseAsync(key, value);
        }

        /// <summary>
        /// 使用锁执行一个方法
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        public static void ExecuteWithLock(string key, string value, TimeSpan span, Action executeAction)
        {
            LockProvider().ExecuteWithLock(key, value, span, executeAction);
        }

        /// <summary>
        /// 使用锁执行一个方法
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        /// <param name="defaultValue">默认返回</param>
        /// <returns></returns>
        public static T ExecuteWithLock<T>(string key, string value, TimeSpan span, Func<T> executeAction, T defaultValue = default(T))
        {
            return LockProvider().ExecuteWithLock(key, value, span, executeAction, defaultValue: defaultValue);
        }

        /// <summary>
        /// 使用锁执行一个异步方法
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        public static Task ExecuteWithLockAsync(string key, string value, TimeSpan span, Func<Task> executeAction)
        {
            return LockProvider().ExecuteWithLockAsync(key, value, span, executeAction);
        }

        /// <summary>
        /// 使用锁执行一个异步方法
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        /// <param name="defaultValue">默认返回</param>
        /// <returns></returns>
        public static Task<T> ExecuteWithLockAsync<T>(string key, string value, TimeSpan span, Func<Task<T>> executeAction, T defaultValue = default(T))
        {
            return LockProvider().ExecuteWithLockAsync(key, value, span, executeAction, defaultValue: defaultValue);
        }
    }
}