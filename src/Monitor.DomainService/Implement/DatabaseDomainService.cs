using JQCore;
using JQCore.Extensions;
using JQCore.Hangfire;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
using System;
using System.Threading.Tasks;
using static Monitor.Constant.LockKeyConstant;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：DatabaseDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库信息业务逻辑处理
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public sealed class DatabaseDomainService : IDatabaseDomainService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IDatabaseCache _databaseCache;

        public DatabaseDomainService(IDatabaseRepository databaseRepository, IDatabaseCache databaseCache)
        {
            _databaseRepository = databaseRepository;
            _databaseCache = databaseCache;
        }

        /// <summary>
        /// 创建数据库信息
        /// </summary>
        /// <param name="databaseMode">数据库信息</param>
        /// <returns>数据库信息</returns>
        public DatabaseInfo Create(DatabaseModel databaseMode)
        {
            databaseMode.NotNull("数据库信息不能为空");
            var databaseInfo = new DatabaseInfo
            {
                FComment = databaseMode.FComment,
                FIsDeleted = false,
                FName = databaseMode.FName,
                FID = databaseMode.FID ?? 0,
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = databaseMode.OperateUserID.Value,
                FDbType = databaseMode.FDbType ?? string.Empty,
                FConnection = databaseMode.FConnection
            };
            if (databaseInfo.FID > 0)
            {
                databaseInfo.FLastModifyTime = DateTimeUtil.Now;
                databaseInfo.FLastModifyUserID = databaseMode.OperateUserID.Value;
            }
            return databaseInfo;
        }

        /// <summary>
        /// 根据数据库名字、类型获取数据库ID
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库ID</returns>
        public async Task<int> GetDatabaseIDAsync(string dbName, string dbType)
        {
            if (dbName.IsNotNullAndNotEmptyWhiteSpace())
            {
                var dbInfo = await _databaseCache.GetDatabaseInfoAsync(dbName.Trim(), dbType ?? string.Empty);
                if (dbInfo == null)
                {
                    dbInfo = await AddWhenNotExistAsync(dbName.Trim(), dbType);
                }
                if (dbInfo != null)
                {
                    return dbInfo.FID;
                }
            }
            return 0;
        }

        /// <summary>
        /// 添加数据库信息,当数据库信息不存在时
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库信息</returns>
        public Task<DatabaseInfo> AddWhenNotExistAsync(string dbName, string dbType)
        {
            dbName.NotNull("数据库名称不能为空");
            dbType.NotNull("数据库类型不能为空");
            var databaseInfo = new DatabaseInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FIsDeleted = false,
                FName = dbName,
                FCreateUserID = -1,
                FDbType = dbType ?? string.Empty
            };
            return LockUtil.ExecuteWithLockAsync(Lock_DatabaseModify, dbName, TimeSpan.FromMinutes(2), async () =>
            {
                var existDbInfo = await _databaseRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FName == dbName && m.FDbType == databaseInfo.FDbType);
                if (existDbInfo != null)
                {
                    return existDbInfo;
                }
                else
                {
                    int dbID = (await _databaseRepository.InsertOneAsync(databaseInfo, keyName: "FID", ignoreFields: IgnoreConstant.FID)).ToSafeInt32(0);
                    databaseInfo.FID = dbID;
                    DatabaseChanged(OperateType.Add, dbID);
                    return databaseInfo;
                }
            });
        }

        /// <summary>
        /// 数据库更新
        /// </summary>
        /// <param name="operateType">操作类型</param>
        /// <param name="dbID">数据库ID</param>
        public void DatabaseChanged(OperateType operateType, int dbID)
        {
            TaskScheldulingUtil.BackGroundJob(() => _databaseCache.DatabaseModifyAsync(dbID));
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="databaseInfo">数据库信息</param>
        /// <returns></returns>
        public async Task CheckAsync(DatabaseInfo databaseInfo)
        {
            databaseInfo.NotNull("数据库信息不能为空");
            //判断是否有存在相同的Mac地址
            var existNameInfo = await _databaseRepository.GetInfoAsync(m => m.FName == databaseInfo.FName && m.FDbType == databaseInfo.FDbType && m.FIsDeleted == false);
            if (existNameInfo != null)
            {
                if (existNameInfo.FID != databaseInfo.FID)
                {
                    throw new BizException("数据库已存在");
                }
            }
        }
    }
}