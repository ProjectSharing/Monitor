using JQCore.Redis;
using Monitor.Domain;
using Monitor.Repository;
using System;
using System.Linq;
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
                var configList = await RedisClient.HashValuesAsync<SysConfigInfo>(Cache_SysConfigList);
                return configList?.Where(m => m.FKey == key).FirstOrDefault()?.FValue;
            }, async () =>
            {
                var sysConfigInfo = await _sysConfigRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FKey == key);
                return sysConfigInfo?.FValue;
            }, memberName: "SysConfigCache-GetValueAsync");
        }

        /// <summary>
        /// 修改配置文件到redis缓存
        /// </summary>
        /// <param name="configID">配置ID</param>
        /// <returns></returns>
        public Task SysConfigModifyAsync(int configID)
        {
            return SetValueAsync(async () =>
            {
                var sysConfigInfo = await _sysConfigRepository.GetInfoAsync(m => m.FID == configID && m.FIsDeleted == false);
                if (sysConfigInfo != null)
                {
                    await RedisClient.HashSetAsync(Cache_SysConfigList, configID.ToString(), sysConfigInfo);
                }
                else
                {
                    if (await RedisClient.HashExistsAsync(Cache_SysConfigList, configID.ToString()))
                    {
                        await RedisClient.HashDeleteAsync(Cache_SysConfigList, configID.ToString());
                    }
                }
            }, memberName: "SysConfigCache-AddSysConfig");
        }

        /// <summary>
        /// 同步系统配置
        /// </summary>
        /// <returns></returns>
        public Task SynchroConfigAsync()
        {
            return SetValueAsync(async () =>
            {
                var lastSynchroTime = await GetLastSynchroTimeAsync(Cache_Synchro_SysConfig);
                var sysConfigList = await _sysConfigRepository.QueryListAsync(m => m.FCreateTime >= lastSynchroTime || m.FLastModifyTime >= lastSynchroTime);
                foreach (var sysConfigInfo in sysConfigList)
                {
                    if (!sysConfigInfo.FIsDeleted)
                    {
                        await RedisClient.HashSetAsync(Cache_SysConfigList, sysConfigInfo.FID.ToString(), sysConfigInfo);
                    }
                    else
                    {
                        if (await RedisClient.HashExistsAsync(Cache_SysConfigList, sysConfigInfo.FID.ToString()))
                        {
                            await RedisClient.HashDeleteAsync(Cache_SysConfigList, sysConfigInfo.FID.ToString());
                        }
                    }
                }
            }, memberName: "SysConfigCache-AddSysConfig");
        }
    }
}