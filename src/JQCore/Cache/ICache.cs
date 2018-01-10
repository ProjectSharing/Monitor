using System;

namespace JQCore.Cache
{
    /// <summary>
    /// Copyright (C) 2018 备胎 版权所有。
    /// 类名：ICache.cs
    /// 类属性：接口
    /// 类功能描述：基础缓存接口类
    /// 创建标识：yjq 2018/1/10 20:04:38
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 添加相对时间缓存
        /// </summary>
        /// <param name="key">需要添加缓存名称</param>
        /// <param name="obj">缓存值</param>
        /// <param name="isSlidCache">是否为相对时间</param>
        void SimpleAddSlidingCache(string key, object obj, TimeSpan timeOutSpan);

        /// <summary>
        /// 添加绝对时间缓存
        /// </summary>
        /// <param name="key">需要添加缓存名称</param>
        /// <param name="obj">缓存值</param>
        /// <param name="time">过期时间</param>
        void SimpleAddAbsoluteCache(string key, object obj, DateTime time);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">需要获取的缓存名称</param>
        /// <returns></returns>
        object SimpleGetCache(string key);

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">需要判断的缓存名称</param>
        /// <returns></returns>
        bool SimpleIsExistCache(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">需要移除的缓存名称</param>
        void SimpleRemoveCache(string key);

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void SimpleRemoveAllCache();
    }
}