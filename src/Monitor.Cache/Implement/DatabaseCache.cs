using JQCore.Extensions;
using JQCore.Redis;
using JQCore.Utils;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;

namespace Monitor.Cache.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：DatabaseCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/12/1 13:35:03
    /// </summary>
    public sealed class DatabaseCache : RedisBaseRepository, IDatabaseCache
    {
        private readonly IDatabaseRepository _databaseRepository;

        public DatabaseCache(IDatabaseRepository databaseRepository, IRedisDatabaseProvider databaseProvider) : base(databaseProvider, RedisOptionProvider.DefaultOption)
        {
            _databaseRepository = databaseRepository;
        }

        /// <summary>
        /// 根据名字或缓存中获取项目信息
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        public Task<DatabaseInfo> GetDatabaseInfoAsync(string databaseName, string dbType)
        {
            return GetValueAsync(async () =>
            {
                var dbList = await RedisClient.HashValuesAsync<DatabaseInfo>(Cache_DbList);
                return dbList.FirstOrDefault(m => m.FName == databaseName && m.FDbType == dbType && m.FIsDeleted == false);
            }, async () =>
            {
                return await _databaseRepository.GetInfoAsync(m => m.FName == databaseName && m.FDbType == dbType && m.FIsDeleted == false);
            }, memberName: "DatabaseCache-GetDatabaseInfoAsync");
        }

        /// <summary>
        /// 获取当前所有数据库列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DatabaseListDto>> GetDatabaseListAsync()
        {
            var dbList = await GetValueAsync(async () =>
            {
                return await RedisClient.HashValuesAsync<DatabaseInfo>(Cache_DbList);
            }, async () =>
            {
                return await _databaseRepository.QueryListAsync(m => m.FIsDeleted == false);
            }, memberName: "DatabaseCache-GetDatabaseListAsync");
            return dbList.Select(m => new DatabaseListDto { FID = m.FID, FComment = m.FComment, FName = m.FName, FDbType = m.FDbType, FLastModifyTime = m.FLastModifyTime ?? m.FCreateTime });
        }

        /// <summary>
        /// 数据库信息发生变化
        /// </summary>
        /// <param name="databaseID">数据库ID</param>
        /// <returns></returns>
        public Task DatabaseModifyAsync(int databaseID)
        {
            return SetValueAsync(async () =>
            {
                var databaseInfo = await _databaseRepository.GetInfoAsync(m => m.FID == databaseID && m.FIsDeleted == false);
                if (databaseInfo != null)
                {
                    await RedisClient.HashSetAsync(Cache_DbList, databaseID.ToString(), databaseInfo);
                }
                else
                {
                    if (await RedisClient.HashExistsAsync(Cache_DbList, databaseID.ToString()))
                    {
                        await RedisClient.HashDeleteAsync(Cache_DbList, databaseID.ToString());
                    }
                }
            }, memberName: "DatabaseCache-DatabaseModifyAsync");
        }

        /// <summary>
        /// 同步数据库信息
        /// </summary>
        /// <returns></returns>
        public Task SynchroDatabaseAsync()
        {
            return SetValueAsync(async () =>
            {
                LogUtil.Info("开始同步数据库信息");
                var lastSynchroTime = await GetLastSynchroTimeAsync(Cache_DbList);
                LogUtil.Info($"数据库信息上次同步时间;{lastSynchroTime.ToDefaultFormat()}");
                var dbList = await _databaseRepository.QueryListAsync();
                LogUtil.Info($"共{(dbList?.Count().ToString()) ?? "0"}条信息需要同步");
                var existsCacheKeyList = RedisClient.HashKeys(Cache_DbList);
                if (existsCacheKeyList != null && existsCacheKeyList.Any())
                {
                    foreach (var existsCacheKey in existsCacheKeyList)
                    {
                        if (!dbList.Any(m => m.FID.ToString() == existsCacheKey))
                        {
                            await RedisClient.HashDeleteAsync(Cache_DbList, existsCacheKey);
                        }
                    }
                }
                foreach (var dbInfo in dbList)
                {
                    if (!dbInfo.FIsDeleted)
                    {
                        await RedisClient.HashSetAsync(Cache_DbList, dbInfo.FID.ToString(), dbInfo);
                    }
                    else
                    {
                        if (await RedisClient.HashExistsAsync(Cache_DbList, dbInfo.FID.ToString()))
                        {
                            await RedisClient.HashDeleteAsync(Cache_DbList, dbInfo.FID.ToString());
                        }
                    }
                    LogUtil.Info($"数据库信息【{dbInfo.FID.ToString()}】同步成功");
                }
                await UpdateLastSynchroTimeAsync(Cache_Synchro_Project);
                LogUtil.Info("同步数据库信息完成");
            }, memberName: "DatabaseCache-SynchroDatabaseAsync");
        }
    }
}