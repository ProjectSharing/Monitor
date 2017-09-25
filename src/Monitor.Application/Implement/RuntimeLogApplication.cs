using Monitor.DomainService;
using Monitor.Repository;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：RuntimeLogApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogApplication : IRuntimeLogApplication
    {
        private readonly IRuntimeLogRepository _runtimeLogRepository;
        private readonly IRuntimeLogDomainService _runtimeLogDomainService;

        public RuntimeLogApplication(IRuntimeLogRepository runtimeLogRepository, IRuntimeLogDomainService runtimeLogDomainService)
        {
            _runtimeLogRepository = runtimeLogRepository;
            _runtimeLogDomainService = runtimeLogDomainService;
        }
    }
}