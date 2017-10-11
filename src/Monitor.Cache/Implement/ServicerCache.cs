using JQCore.Redis;
using Monitor.Domain;
using Monitor.Repository;
using System.Linq;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;

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
            }, memberName: "ProjectCache-GetProjectInfo");
        }
    }
}