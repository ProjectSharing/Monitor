using JQCore;
using JQCore.Extensions;
using JQCore.Result;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;
using static Monitor.Constant.LockKeyConstant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：DatabaseApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库信息业务逻辑
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public sealed class DatabaseApplication : IDatabaseApplication
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IDatabaseDomainService _databaseDomainService;
        private readonly IDatabaseCache _databaseCache;
        private readonly IOperateLogDomainService _operateLogDomainService;

        public DatabaseApplication(IDatabaseRepository databaseRepository, IDatabaseDomainService databaseDomainService, IDatabaseCache databaseCache, IOperateLogDomainService operateLogDomainService)
        {
            _databaseRepository = databaseRepository;
            _databaseDomainService = databaseDomainService;
            _databaseCache = databaseCache;
            _operateLogDomainService = operateLogDomainService;
        }

        /// <summary>
        /// 分页获取数据库列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>数据库列表</returns>
        public Task<OperateResult<IPageResult<DatabaseListDto>>> GetDbListAsync(DatabasePageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _databaseRepository.GetDbListAsync(queryWhere);
            }, callMemberName: "DatabaseApplication-GetDbListAsync");
        }

        /// <summary>
        /// 同步数据库信息
        /// </summary>
        /// <returns>操作结果</returns>
        public Task<OperateResult> SynchroDatabaseAsync()
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                await _databaseCache.SynchroDatabaseAsync();
            }, callMemberName: "DatabaseApplication-SynchroDatabaseAsync");
        }

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <param name="databaseModel">数据库信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddDatabaseAsync(DatabaseModel databaseModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var dbInfo = _databaseDomainService.Create(databaseModel);
                int id = await LockUtil.ExecuteWithLockAsync(Lock_DatabaseModify, dbInfo.FName, TimeSpan.FromMinutes(2), async () =>
                {
                    await _databaseDomainService.CheckAsync(dbInfo);
                    int dbID = (await _databaseRepository.InsertOneAsync(dbInfo, keyName: "FID", ignoreFields: FID)).ToSafeInt32(0);
                    _operateLogDomainService.AddOperateLog(databaseModel.OperateUserID.Value, OperateModule.Database, OperateModuleNode.Add, $"添加:{dbInfo.GetOperateDesc()}");
                    _databaseDomainService.DatabaseChanged(OperateType.Add, dbID);
                    return dbID;
                }, defaultValue: -1);
                if (id <= 0)
                {
                    throw new BizException("添加失败");
                }
            }, callMemberName: "DatabaseApplication-AddDatabaseAsync");
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="dbID">数据库ID</param>
        /// <returns>数据库信息</returns>
        public Task<OperateResult<DatabaseModel>> GetDatabaseModelAsync(int dbID)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _databaseRepository.GetDtoAsync<DatabaseModel>(m => m.FID == dbID && m.FIsDeleted == false, ignoreFields: OperaterUserID);
            }, callMemberName: "DatabaseApplication-GetDatabaseModelAsync");
        }

        /// <summary>
        /// 修改数据库信息
        /// </summary>
        /// <param name="databaseModel">数据库信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> EditDatabaseAsync(DatabaseModel databaseModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var dbInfo = _databaseDomainService.Create(databaseModel);
                var flag = await LockUtil.ExecuteWithLockAsync(Lock_DatabaseModify, dbInfo.FName, TimeSpan.FromMinutes(2), async () =>
                {
                    await _databaseDomainService.CheckAsync(dbInfo);
                    await _databaseRepository.UpdateAsync(dbInfo, m => m.FID == dbInfo.FID, ignoreFields: IDAndCreate);
                    _operateLogDomainService.AddOperateLog(databaseModel.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Edit, $"{dbInfo.GetOperateDesc()}");
                    _databaseDomainService.DatabaseChanged(OperateType.Modify, dbInfo.FID);
                    return true;
                }, defaultValue: false);
                if (!flag)
                {
                    throw new BizException("修改失败");
                }
            }, callMemberName: "DatabaseApplication-EditProjectAsync");
        }

        /// <summary>
        /// 加载全部的数据库信息
        /// </summary>
        /// <returns></returns>
        public Task<OperateResult<IEnumerable<DatabaseListDto>>> LoadDbListAsync()
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _databaseCache.GetDatabaseListAsync();
            }, callMemberName: "DatabaseApplication-LoadDbListAsync");
        }

        /// <summary>
        /// 获取表结构列表
        /// </summary>
        /// <param name="dbID">数据库ID</param>
        /// <returns>表结构列表</returns>
        public Task<OperateResult<IEnumerable<TableStructureDto>>> GetTableStructureListAsync(int dbID)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _databaseRepository.GetTableStructureListAsync(dbID);
            }, callMemberName: "DatabaseApplication-GetTableStructureListAsync");
        }
    }
}