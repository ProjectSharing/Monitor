using JQCore.Redis;
using JQCore.Utils;
using Monitor.Domain;
using Monitor.Repository;
using System.Linq;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;
using JQCore.Extensions;
using System.Collections.Generic;
using Monitor.Trans;

namespace Monitor.Cache.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ServicerCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器缓存
    /// 创建标识：yjq 2017/10/9 14:53:45
    /// </summary>
    public sealed class ServicerCache : RedisBaseRepository, IServicerCache
    {
        private readonly IServicerRepository _servicerRepository;

        public ServicerCache(IServicerRepository servicerRepository, IRedisDatabaseProvider databaseProvider) : base(databaseProvider, RedisOptionProvider.DefaultOption)
        {
            _servicerRepository = servicerRepository;
        }

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="servicerMac">服务器Mac地址</param>
        /// <returns>服务器信息</returns>
        public Task<ServicerInfo> GetServicerInfoAsync(string servicerMac)
        {
            return GetValueAsync(async () =>
            {
                var servicerList = await RedisClient.HashValuesAsync<ServicerInfo>(Cache_ServicerList);
                return servicerList.FirstOrDefault(m => m.FMacAddress == servicerMac && m.FIsDeleted == false);
            }, async () =>
            {
                return await _servicerRepository.GetInfoAsync(m => m.FMacAddress == servicerMac);
            }, memberName: "ServicerCache-GetProjectInfo");
        }

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <returns>服务器列表</returns>
        public async Task<IEnumerable<ServicerListDto>> GetServicerListAsync()
        {
            var servicerList = await GetValueAsync(async () =>
            {
                return await RedisClient.HashValuesAsync<ServicerInfo>(Cache_ServicerList);
            }, async () =>
            {
                return await _servicerRepository.QueryListAsync(m => m.FIsDeleted == false);
            }, memberName: "ProjectCache-GetProjectInfoAsync");
            return servicerList.Select(m => new ServicerListDto
            {
                FID = m.FID,
                FComment = m.FComment,
                FName = m.FName,
                FLastModifyTime = m.FLastModifyTime ?? m.FCreateTime,
                FIP = m.FIP,
                FMacAddress = m.FMacAddress
            });
        }

        /// <summary>
        /// 修改服务器到redis缓存
        /// </summary>
        /// <param name="serviserID">服务器ID</param>
        /// <returns></returns>
        public Task ServicerModifyAsync(int serviserID)
        {
            return SetValueAsync(async () =>
            {
                var servicerInfo = await _servicerRepository.GetInfoAsync(m => m.FID == serviserID && m.FIsDeleted == false);
                if (servicerInfo != null)
                {
                    await RedisClient.HashSetAsync(Cache_ServicerList, serviserID.ToString(), servicerInfo);
                }
                else
                {
                    if (await RedisClient.HashExistsAsync(Cache_ServicerList, serviserID.ToString()))
                    {
                        await RedisClient.HashDeleteAsync(Cache_ServicerList, serviserID.ToString());
                    }
                }
            }, memberName: "ServicerCache-ServicerModifyAsync");
        }

        /// <summary>
        /// 同步服务器信息
        /// </summary>
        /// <returns></returns>
        public Task SynchroServiceAsync()
        {
            return SetValueAsync(async () =>
            {
                LogUtil.Info("开始同步服务器信息");
                var lastSynchroTime = await GetLastSynchroTimeAsync(Cache_Synchro_Service);
                LogUtil.Info($"服务器信息上次同步时间;{lastSynchroTime.ToDefaultFormat()}");
                var serviceList = await _servicerRepository.QueryListAsync();
                LogUtil.Info($"共{(serviceList?.Count().ToString()) ?? "0"}条信息需要同步");
                var existsCacheKeyList = RedisClient.HashKeys(Cache_ServicerList);
                if (existsCacheKeyList != null && existsCacheKeyList.Any())
                {
                    foreach (var existsCacheKey in existsCacheKeyList)
                    {
                        if (!serviceList.Any(m => m.FID.ToString() == existsCacheKey))
                        {
                            await RedisClient.HashDeleteAsync(Cache_ServicerList, existsCacheKey);
                        }
                    }
                }
                foreach (var servicerInfo in serviceList)
                {
                    if (!servicerInfo.FIsDeleted)
                    {
                        await RedisClient.HashSetAsync(Cache_ServicerList, servicerInfo.FID.ToString(), servicerInfo);
                    }
                    else
                    {
                        if (await RedisClient.HashExistsAsync(Cache_ServicerList, servicerInfo.FID.ToString()))
                        {
                            await RedisClient.HashDeleteAsync(Cache_ServicerList, servicerInfo.FID.ToString());
                        }
                    }
                    LogUtil.Info($"服务器信息【{servicerInfo.FID.ToString()}】同步成功");
                }
                await UpdateLastSynchroTimeAsync(Cache_Synchro_Service);
                LogUtil.Info("服务器信息完成");
            }, memberName: "ServicerCache-SynchroConfigAsync");
        }
    }
}