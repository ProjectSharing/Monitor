using JQCore.Redis;
using Monitor.Repository;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;

namespace Monitor.Cache.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SysConfigCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/10 13:36:20
    /// </summary>
    public class SysConfigCache : RedisBaseRepository, ISysConfigCache
    {
        private readonly ISysConfigRepository _sysConfigRepository;

        public SysConfigCache(ISysConfigRepository sysConfigRepository, IRedisDatabaseProvider databaseProvider) : base(databaseProvider, RedisOptionProvider.DefaultOption)
        {
            _sysConfigRepository = sysConfigRepository;
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置的Key</param>
        /// <returns>配置值</returns>
        public Task<string> GetValueAsync(string key)
        {
            return GetValueAsync(async () =>
            {
                return await RedisClient.HashGetAsync<string>(Cache_SysConfigList, key);
            }, async () =>
            {
                var sysConfigInfo = await _sysConfigRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FKey == key);
                return sysConfigInfo?.FValue;
            }, memberName: "SysConfigCache-GetValueAsync");
        }
    }
}