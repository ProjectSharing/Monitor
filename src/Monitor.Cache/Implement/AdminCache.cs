using Microsoft.Extensions.Caching.Memory;
using System;
using static Monitor.Constant.CacheKeyConstant;

namespace Monitor.Cache.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：AdminCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/25 10:31:26
    /// </summary>
    public class AdminCache : IAdminCache
    {
        private readonly IMemoryCache _memoryCache;

        public AdminCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取尝试登录次数
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>尝试登录次数</returns>
        public int GetTryLoginCount(string userName)
        {
            string key = $"{Cache_Admin_TryLoginCount}{userName}";
            _memoryCache.TryGetValue(key, out int tryCount);
            _memoryCache.Set(key, tryCount + 1, TimeSpan.FromMinutes(2));
            return tryCount;
        }

        /// <summary>
        /// 清除用户尝试登录的次数
        /// </summary>
        /// <param name="userName">用户名</param>
        public void ClearLoginCount(string userName)
        {
            string key = $"{Cache_Admin_TryLoginCount}{userName}";
            _memoryCache.Remove(key);
        }
    }
}