using JQCore.Result;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;
using JQCore.Utils;
using Monitor.Constant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：WarningLogApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志预警信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningLogApplication : IWarningLogApplication
    {
        private readonly IWarningLogRepository _warningLogRepository;
        private readonly IWarningLogDomainService _warningLogDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;
        private readonly IWarningInfoOperateLogDomainService _warningInfoOperateLogDomainService;

        public WarningLogApplication(IWarningLogRepository warningLogRepository, IWarningLogDomainService warningLogDomainService, IOperateLogDomainService operateLogDomainService, IWarningInfoOperateLogDomainService warningInfoOperateLogDomainService)
        {
            _warningLogRepository = warningLogRepository;
            _warningLogDomainService = warningLogDomainService;
            _operateLogDomainService = operateLogDomainService;
            _warningInfoOperateLogDomainService = warningInfoOperateLogDomainService;
        }

        /// <summary>
        /// 异步查找警告日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>警告日志列表</returns>
        public Task<OperateResult<IPageResult<WarningLogListDto>>> GetWarningLogListAsync(WarningLogPageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _warningLogRepository.GetWarningLogListAsync(queryWhere);
            }, callMemberName: "WarningLogApplication-GetWarningLogListAsync");
        }

        /// <summary>
        /// 获取处理对象
        /// </summary>
        /// <param name="id">警告日志记录ID</param>
        /// <returns></returns>
        public Task<OperateResult<WarningLogDealModel>> GetDealModelAsync(int id)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _warningLogRepository.GetWarningLogDealModel(id);
            }, callMemberName: "WarningLogApplication-GetDealModelAsync");
        }

        /// <summary>
        /// 处理警告日志日志
        /// </summary>
        /// <param name="model">处理信息</param>
        /// <returns></returns>
        public Task<OperateResult> DealAsync(WarningLogDealModel model)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                model.NotNull("处理信息不能为空");
                model.FTreatmentScheme.NotNullAndNotEmptyWhiteSpace("处理方案不能为空");
                await _warningLogRepository.UpdateAsync(new
                {
                    FTreatmentScheme = model.FTreatmentScheme,
                    FDealState = DealState.Dealed,
                    FLastModifyTime = DateTimeUtil.Now,
                    FLastModifyUserID = model.OperateUserID.Value
                }, m => m.FID == model.FID);
                await _warningInfoOperateLogDomainService.AddLogAsync(1, model.FID, model.FTreatmentScheme, model.OperateUserID.Value);
                _operateLogDomainService.AddOperateLog(model.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Edit, $"处理警告日志");
            }, callMemberName: "WarningLogApplication-DealAsync");
        }
    }
}