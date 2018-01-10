using Microsoft.Extensions.Caching.Memory;
using System;

namespace JQCore.Cache
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：LocalCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：本地缓存信息
    /// 创建标识：yjq 2018/1/10 20:05:01
    /// </summary>
    public sealed class LocalCache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public LocalCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 添加相对时间缓存
        /// </summary>
        /// <param name="key">需要添加缓存名称</param>
        /// <param name="obj">缓存值</param>
        /// <param name="isSlidCache">是否为相对时间</param>
        public void SimpleAddSlidingCache(string key, object obj, TimeSpan timeOutSpan)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            SimpleRemoveCache(key);//如果原先存在则先移除
            _memoryCache.Set(key, obj, timeOutSpan);
        }

        /// <summary>
        /// 添加绝对时间缓存
        /// </summary>
        /// <param name="key">需要添加缓存名称</param>
        /// <param name="obj">缓存值</param>
        /// <param name="time">过期时间</param>
        public void SimpleAddAbsoluteCache(string key, object obj, DateTime time)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            SimpleRemoveCache(key);//如果原先存在则先移除
            _memoryCache.Set(key, obj, time);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">需要获取的缓存名称</param>
        /// <returns></returns>
        public object SimpleGetCache(string key)
        {
            return SimpleIsExistCache(key) ? _memoryCache.Get(key) : null;
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">需要判断的缓存名称</param>
        /// <returns></returns>
        public bool SimpleIsExistCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return false;
            return _memoryCache.Get(key) == null ? false : true;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">需要移除的缓存名称</param>
        public void SimpleRemoveCache(string key)
        {
            if (SimpleIsExistCache(key)) _memoryCache.Remove(key);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void SimpleRemoveAllCache()
        {
        }
    }
}