using Monitor.DomainService;
using Monitor.Repository;

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

        public WarningLogApplication(IWarningLogRepository warningLogRepository, IWarningLogDomainService warningLogDomainService)
        {
            _warningLogRepository = warningLogRepository;
            _warningLogDomainService = warningLogDomainService;
        }
    }
}