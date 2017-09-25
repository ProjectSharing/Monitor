using Monitor.DomainService;
using Monitor.Repository;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：OperateLogApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员操作记录业务逻辑
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class OperateLogApplication : IOperateLogApplication
    {
        private readonly IOperateLogRepository _operateLogRepository;
        private readonly IOperateLogDomainService _operateLogDomainService;

        public OperateLogApplication(IOperateLogRepository operateLogRepository, IOperateLogDomainService operateLogDomainService)
        {
            _operateLogRepository = operateLogRepository;
            _operateLogDomainService = operateLogDomainService;
        }
    }
}