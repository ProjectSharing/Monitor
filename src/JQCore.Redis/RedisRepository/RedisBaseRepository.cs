using JQCore.Utils;
using System;
using System.Threading.Tasks;

namespace JQCore.Redis
{
    /// <summary>
    /// Copyright (C) 2015 备胎 版权所有。
    /// 类名：RedisBaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Redis缓存访问基础类
    /// 创建标识：yjq 2017/7/15 20:36:20
    /// </summary>
    public class RedisBaseRepository
    {
        private readonly IRedisDatabaseProvider _databaseProvider;
        private readonly RedisCacheOption _cacheOption;
        private IRedisClient _clien;

        public RedisBaseRepository(IRedisDatabaseProvider databaseProvider, RedisCacheOption redisCacheOption)
        {
            EnsureUtil.NotNull(databaseProvider, "IRedisDatabaseProvider");
            EnsureUtil.NotNull(redisCacheOption, "RedisCacheOption");
            _databaseProvider = databaseProvider;
            _cacheOption = redisCacheOption;
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        protected RedisCacheOption CacheOption
        {
            get
            {
                return _cacheOption;
            }
        }

        /// <summary>
        /// Redis实例
        /// </summary>
        public IRedisClient RedisClient
        {
            get
            {
                return _clien ?? (_clien = _databaseProvider.CreateClient(_cacheOption));
            }
        }

        /// <summary>
        /// 设置值,异常时则直接忽略
        /// </summary>
        /// <param name="cacheSetAction">设置值的方法</param>
        /// <param name="memberName">调用方法名字</param>
        protected void SetValue(Action cacheSetAction, string memberName = null)
        {
            try
            {
                cacheSetAction();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
            }
        }

        /// <summary>
        /// 设置值,异常时则直接忽略
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="cacheSetAction">设置值的方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>成功时返回值,失败时返回默认值</returns>
        protected T SetValue<T>(Func<T> cacheSetAction, T defaultValue, string memberName = null)
        {
            try
            {
                return cacheSetAction();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return defaultValue;
            }
        }

        /// <summary>
        /// 异步设置值,异常时则直接忽略
        /// </summary>
        /// <param name="cacheSetActionAsync">设置值的异步方法</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>任务，可等待</returns>
        protected async Task SetValueAsync(Func<Task> cacheSetActionAsync, string memberName = null)
        {
            try
            {
                await cacheSetActionAsync();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
            }
        }

        /// <summary>
        /// 异步设置值,异常时则直接忽略
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <param name="cacheSetActionAsync">设置值的异步方法</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>成功时返回值,失败时返回默认值</returns>
        protected async Task<T> SetValueAsync<T>(Func<Task<T>> cacheSetActionAsync, T defaultValue, string memberName = null)
        {
            try
            {
                return await cacheSetActionAsync();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取值(缓存异常时则从db获取)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="cacheGetAction">缓存获取的方法</param>
        /// <param name="dbGetAction">db获取方法</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected T GetValue<T>(Func<T> cacheGetAction, Func<T> dbGetAction, string memberName = null)
        {
            try
            {
                return cacheGetAction();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return dbGetAction();
            }
        }

        /// <summary>
        /// 获取值(缓存异常时则获取默认值)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="cacheGetAction">缓存获取的方法</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected T GetValue<T>(Func<T> cacheGetAction, T defaultValue, string memberName = null)
        {
            try
            {
                return cacheGetAction();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return defaultValue;
            }
        }

        /// <summary>
        /// 异步获取值(缓存异常时则从db获取)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="cacheGetActionAsync">缓存获取的异步方法</param>
        /// <param name="dbGetActionAsync">db获取异步方法</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected async Task<T> GetValueAsync<T>(Func<Task<T>> cacheGetActionAsync, Func<Task<T>> dbGetActionAsync, string memberName = null)
        {
            try
            {
                return await cacheGetActionAsync();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return await dbGetActionAsync();
            }
        }

        /// <summary>
        /// 异步获取值(缓存异常时则获取默认值)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="cacheGetActionAsync">缓存获取的异步方法</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected async Task<T> GetValueAsync<T>(Func<Task<T>> cacheGetActionAsync, T defaultValue, string memberName = null)
        {
            try
            {
                return await cacheGetActionAsync();
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取或者设置值(缓存异常时则从db获取)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="cacheGetAction">缓存获取的方法</param>
        /// <param name="cacheSetAction">缓存设置的方法</param>
        /// <param name="dbGetAction">db获取方法</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected T GetValueWhenNotExitThenSet<T>(string key, Func<string, T> cacheGetAction, Action<string, T> cacheSetAction, Func<T> dbGetAction, string memberName = null)
        {
            try
            {
                if (RedisClient.Exists(key))
                {
                    return cacheGetAction(key);
                }
                else
                {
                    var value = dbGetAction();
                    cacheSetAction(key, value);
                    return value;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return dbGetAction();
            }
        }

        /// <summary>
        /// 异步获取或者设置值(缓存异常时则从db获取)
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="cacheGetActionAsync">缓存获取的异步方法</param>
        /// <param name="cacheSetActionAsync">缓存设置的异步方法</param>
        /// <param name="dbGetActionAsync">db获取的异步方法</param>
        /// <param name="memberName">调用方法名字</param>
        /// <returns>值</returns>
        protected async Task<T> GetValueWhenNotExitThenSetAsync<T>(string key, Func<string, Task<T>> cacheGetActionAsync, Action<string, T> cacheSetActionAsync, Func<Task<T>> dbGetActionAsync, string memberName = null)
        {
            try
            {
                if (RedisClient.Exists(key))
                {
                    return await cacheGetActionAsync(key);
                }
                else
                {
                    var value = await dbGetActionAsync();
                    cacheSetActionAsync(key, value);
                    return value;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: memberName);
                return await dbGetActionAsync();
            }
        }

        /// <summary>
        /// 获取业务上次同步时间
        /// </summary>
        /// <param name="key">业务对应的Key</param>
        /// <returns>上次同步时间</returns>
        protected Task<DateTime> GetLastSynchroTimeAsync(string key)
        {
            return GetValueAsync(async () =>
            {
                var lastSynchroTime = (await RedisClient.HashGetAsync<DateTime?>("CacheSynchroList", key)) ?? DateTime.Parse("1970-01-01");
                return lastSynchroTime;
            }, defaultValue: DateTime.MinValue, memberName: "GetLastSynchroTime");
        }

        /// <summary>
        /// 获取业务上次同步时间
        /// </summary>
        /// <param name="key">业务对应的Key</param>
        /// <returns>上次同步时间</returns>
        protected DateTime GetLastSynchroTime(string key)
        {
            return GetValue(() =>
           {
               var lastSynchroTime = (RedisClient.HashGet<DateTime?>("CacheSynchroList", key)) ?? DateTime.Parse("1970-01-01");
               return lastSynchroTime;
           }, defaultValue: DateTime.MinValue, memberName: "GetLastSynchroTime");
        }

        /// <summary>
        /// 更新上次同步时间
        /// </summary>
        /// <param name="key">业务对应的Key</param>
        /// <returns></returns>
        protected Task UpdateLastSynchroTimeAsync(string key)
        {
            return SetValueAsync(async () =>
            {
                await RedisClient.HashSetAsync("CacheSynchroList", key, DateTime.Now);
            }, memberName: "UpdateLastSynchroTimeAsync");
        }

        /// <summary>
        /// 更新上次同步时间
        /// </summary>
        /// <param name="key">业务对应的Key</param>
        protected void UpdateLastSynchroTime(string key)
        {
            SetValue(() =>
           {
               RedisClient.HashSet("CacheSynchroList", key, DateTime.Now);
           }, memberName: "UpdateLastSynchroTime");
        }
    }
}