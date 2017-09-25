using Monitor.DomainService;
using Monitor.Repository;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：WarningInfoOperateLogApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：预警信息操作记录业务逻辑
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningInfoOperateLogApplication : IWarningInfoOperateLogApplication
    {
        private readonly IWarningInfoOperateLogRepository _warningInfoOperateLogRepository;
        private readonly IWarningInfoOperateLogDomainService _warningInfoOperateLogDomainService;

        public WarningInfoOperateLogApplication(IWarningInfoOperateLogRepository warningInfoOperateLogRepository, IWarningInfoOperateLogDomainService warningInfoOperateLogDomainService)
        {
            _warningInfoOperateLogRepository = warningInfoOperateLogRepository;
            _warningInfoOperateLogDomainService = warningInfoOperateLogDomainService;
        }
    }
}