using Monitor.DomainService;
using Monitor.Repository;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：AdminDetailApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员详情信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminDetailApplication : IAdminDetailApplication
    {
        private readonly IAdminDetailRepository _adminDetailRepository;
        private readonly IAdminDetailDomainService _adminDetailDomainService;

        public AdminDetailApplication(IAdminDetailRepository adminDetailRepository, IAdminDetailDomainService adminDetailDomainService)
        {
            _adminDetailRepository = adminDetailRepository;
            _adminDetailDomainService = adminDetailDomainService;
        }
    }
}