using JQCore.Extensions;
using JQCore.Utils;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace JQCore.Lock
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：LocalLock.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：LocalLock
    /// 创建标识：yjq 2017/9/21 22:29:15
    /// </summary>
    public sealed class LocalLock : ILock
    {
        private static ConcurrentDictionary<string, object> _LockCache = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, string> _LockUserCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 获取一个锁(需要自己释放)
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <returns>成功返回true</returns>
        public bool LockTake(string key, string value, TimeSpan span)
        {
            EnsureUtil.NotNullAndNotEmpty(key, "Lockkey");
            EnsureUtil.NotNullAndNotEmpty(value, "Lockvalue");
            var obj = _LockCache.GetValue(key, () => { return new object(); });
            Monitor.Enter(obj);
            _LockUserCache[key] = value;
            LogUtil.Debug($"{key}:{value}获取锁成功");
            return true;
        }

        /// <summary>
        /// 释放一个锁
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <returns>成功返回true</returns>
        public bool LockRelease(string key, string value)
        {
            EnsureUtil.NotNullAndNotEmpty(key, "Lockkey");
            EnsureUtil.NotNullAndNotEmpty(value, "Lockvalue");
            _LockCache.TryGetValue(key, out object obj);
            if (obj != null)
            {
                if (_LockUserCache[key] == value)
                {
                    Monitor.Exit(obj);
                    LogUtil.Debug($"{key}:{value}释放锁成功");
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// 使用锁执行一个方法
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        public void ExecuteWithLock(string key, string value, TimeSpan span, Action executeAction)
        {
            if (executeAction == null) return;
            if (LockTake(key, value, span))
            {
                try
                {
                    executeAction();
                }
                finally
                {
                    LockRelease(key, value);
                }
            }
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
        public T ExecuteWithLock<T>(string key, string value, TimeSpan span, Func<T> executeAction, T defaultValue = default(T))
        {
            if (executeAction == null) return defaultValue;
            if (LockTake(key, value, span))
            {
                try
                {
                    return executeAction();
                }
                finally
                {
                    LockRelease(key, value);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 使用锁执行一个异步方法
        /// </summary>
        /// <param name="key">锁的键</param>
        /// <param name="value">当前占用值</param>
        /// <param name="span">耗时时间</param>
        /// <param name="executeAction">要执行的方法</param>
        public async Task ExecuteWithLockAsync(string key, string value, TimeSpan span, Func<Task> executeAction)
        {
            if (executeAction == null) return;
            if (LockTake(key, value, span))
            {
                try
                {
                    await executeAction().ContinueWith((task) => { LockRelease(key, value); });
                }
                catch
                {
                    LockRelease(key, value);
                    throw;
                }
            }
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
        public async Task<T> ExecuteWithLockAsync<T>(string key, string value, TimeSpan span, Func<Task<T>> executeAction, T defaultValue = default(T))
        {
            if (executeAction == null) return defaultValue;
            if (LockTake(key, value, span))
            {
                try
                {
                    return await executeAction().ContinueWith((task) =>
                    {
                        LockRelease(key, value);
                        return task.Result;
                    });
                }
                catch
                {
                    LockRelease(key, value);
                    throw;
                }
            }
            return defaultValue;
        }
    }
}