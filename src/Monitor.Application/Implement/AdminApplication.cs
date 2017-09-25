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
    /// 类名：AdminApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员业务逻辑
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminApplication : IAdminApplication
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAdminDomainService _adminDomainService;
        private readonly IAdminDetailDomainService _adminDetailDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;

        public AdminApplication(IAdminRepository adminRepository, IAdminDomainService adminDomainService, IAdminDetailDomainService adminDetailDomainService, IOperateLogDomainService operateLogDomainService)
        {
            _adminRepository = adminRepository;
            _adminDomainService = adminDomainService;
            _adminDetailDomainService = adminDetailDomainService;
            _operateLogDomainService = operateLogDomainService;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">用户登录信息</param>
        /// <returns>登陆成功则返回用户ID</returns>
        public Task<OperateResult<int>> LoginAsync(LoginModel model)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                model.NotNull("请输入您的信息");
                var adminInfo = await _adminDomainService.LoginAsync(model.UserName, model.Pwd);
                await _adminDetailDomainService.ChangeLastLoginInfoAsync(adminInfo);
                _operateLogDomainService.AddOperateLog(adminInfo.FID, OperateModule.User, OperateModuleNode.Login, "用户登录");
                return adminInfo.FID;
            }, callMemberName: "AdminApplication-LoginAsync");
        }
    }
}