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
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;
using static Monitor.Constant.LockKeyConstant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：SysConfigApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：系统配置信息业务逻辑
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public sealed class SysConfigApplication : ISysConfigApplication
    {
        private readonly ISysConfigRepository _sysConfigRepository;
        private readonly ISysConfigDomainService _sysConfigDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;
        private readonly ISysConfigCache _sysConfigCache;

        public SysConfigApplication(ISysConfigRepository sysConfigRepository, ISysConfigDomainService sysConfigDomainService, IOperateLogDomainService operateLogDomainService, ISysConfigCache sysConfigCache)
        {
            _sysConfigRepository = sysConfigRepository;
            _sysConfigDomainService = sysConfigDomainService;
            _operateLogDomainService = operateLogDomainService;
            _sysConfigCache = sysConfigCache;
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>配置列表</returns>
        public Task<OperateResult<IPageResult<SysConfigListDto>>> GetConfigListAsync(SysConfigPageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _sysConfigRepository.GetConfigListAsync(queryWhere);
            }, callMemberName: "SysConfigApplication-GetConfigListAsync");
        }

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddConfigAsync(SysConfigModel sysConfigModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var sysConfigInfo = _sysConfigDomainService.Create(sysConfigModel);
                int id = await LockUtil.ExecuteWithLockAsync(Lock_SysConfigModify, sysConfigInfo.FKey, TimeSpan.FromMinutes(2), async () =>
                {
                    await _sysConfigDomainService.CheckAsync(sysConfigInfo);
                    var configID = (await _sysConfigRepository.InsertOneAsync(sysConfigInfo, keyName: "FID", ignoreFields: FID)).ToSafeInt32(0);
                    _operateLogDomainService.AddOperateLog(sysConfigModel.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Add, $"{sysConfigInfo.GetOperateDesc()}");
                    _sysConfigDomainService.ConfigChanged(OperateType.Add, configID);
                    return configID;
                }, defaultValue: -1);
                if (id <= 0)
                {
                    throw new BizException("添加失败");
                }
            }, callMemberName: "SysConfigApplication-AddConfigAsync");
        }

        /// <summary>
        /// 获取配置编辑信息
        /// </summary>
        /// <param name="configID">配置ID</param>
        /// <returns>配置编辑信息</returns>
        public Task<OperateResult<SysConfigModel>> GetSysConfigModelAsync(int configID)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _sysConfigRepository.GetDtoAsync<SysConfigModel>(m => m.FID == configID && m.FIsDeleted == false, ignoreFields: OperaterUserID);
            }, callMemberName: "SysConfigApplication-GetSysConfigModelAsync");
        }

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> EditConfigAsync(SysConfigModel sysConfigModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var sysConfigInfo = _sysConfigDomainService.Create(sysConfigModel);
                var flag = await LockUtil.ExecuteWithLockAsync(Lock_SysConfigModify, sysConfigInfo.FKey, TimeSpan.FromMinutes(2), async () =>
                {
                    await _sysConfigDomainService.CheckAsync(sysConfigInfo);
                    await _sysConfigRepository.UpdateAsync(sysConfigInfo, m => m.FID == sysConfigInfo.FID, ignoreFields: IDAndCreate);
                    _operateLogDomainService.AddOperateLog(sysConfigModel.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Edit, $"{sysConfigInfo.GetOperateDesc()}");
                    _sysConfigDomainService.ConfigChanged(OperateType.Modify, sysConfigInfo.FID);
                    return true;
                }, defaultValue: false);
                if (!flag)
                {
                    throw new BizException("修改失败");
                }
            }, callMemberName: "SysConfigApplication-EditConfigAsync");
        }

        /// <summary>
        /// 同步配置信息
        /// </summary>
        /// <returns>操作结果</returns>
        public Task<OperateResult> SynchroConfigAsync()
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                await _sysConfigCache.SynchroConfigAsync();
            }, callMemberName: "SysConfigApplication-SynchroConfigAsync");
        }
    }
}