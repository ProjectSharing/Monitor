using JQCore.Result;
using JQCore.Utils;
using Monitor.Constant;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：WarningSqlApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL预警信息业务逻辑
    /// 创建标识：template 2017-11-29 22:17:29
    /// </summary>
    public sealed class WarningSqlApplication : IWarningSqlApplication
    {
        private readonly IWarningSqlRepository _warningSqlRepository;
        private readonly IWarningSqlDomainService _warningSqlDomainService;
        private readonly IWarningInfoOperateLogDomainService _warningInfoOperateLogDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;

        public WarningSqlApplication(IWarningSqlRepository warningSqlRepository, IWarningSqlDomainService warningSqlDomainService, IWarningInfoOperateLogDomainService warningInfoOperateLogDomainService, IOperateLogDomainService operateLogDomainService)
        {
            _warningSqlRepository = warningSqlRepository;
            _warningSqlDomainService = warningSqlDomainService;
            _warningInfoOperateLogDomainService = warningInfoOperateLogDomainService;
            _operateLogDomainService = operateLogDomainService;
        }

        /// <summary>
        /// 获取预警日志列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        public Task<OperateResult<IPageResult<WarningSqlListDto>>> GetWarningSqlListAsync(WarningSqlPageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _warningSqlRepository.GetWarningSqlListAsync(queryWhere);
            }, callMemberName: "WarningSqlApplication-GetWarningSqlListAsync");
        }

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<OperateResult<WarningDealModel>> GetWarningDealModelAsync(int id)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _warningSqlRepository.GetWarningDealModelAsync(id);
            }, callMemberName: "WarningSqlApplication-GetWarningDealModelAsync");
        }

        /// <summary>
        /// 处理警告日志日志
        /// </summary>
        /// <param name="model">处理信息</param>
        /// <returns></returns>
        public Task<OperateResult> DealAsync(WarningDealModel model)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                model.NotNull("处理信息不能为空");
                model.FTreatmentScheme.NotNullAndNotEmptyWhiteSpace("处理方案不能为空");
                await _warningSqlRepository.UpdateAsync(new
                {
                    FTreatmentScheme = model.FTreatmentScheme,
                    FDealState = DealState.Dealed,
                    FLastModifyTime = DateTimeUtil.Now,
                    FLastModifyUserID = model.OperateUserID.Value
                }, m => m.FID == model.FID);
                await _warningInfoOperateLogDomainService.AddLogAsync(1, model.FID, model.FTreatmentScheme, model.OperateUserID.Value);
                _operateLogDomainService.AddOperateLog(model.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Edit, $"处理预警SQL");
            }, callMemberName: "WarningSqlApplication-DealAsync");
        }
    }
}