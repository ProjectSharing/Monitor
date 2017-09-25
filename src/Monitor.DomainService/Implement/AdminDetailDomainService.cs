using JQCore.Utils;
using JQCore.Web;
using Monitor.Domain;
using Monitor.Repository;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：AdminDetailDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员详情信息业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminDetailDomainService : IAdminDetailDomainService
    {
        private readonly IAdminDetailRepository _adminDetailRepository;

        public AdminDetailDomainService(IAdminDetailRepository adminDetailRepository)
        {
            _adminDetailRepository = adminDetailRepository;
        }

        /// <summary>
        /// 更新用户上次登录的信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public async Task ChangeLastLoginInfoAsync(AdminInfo adminInfo)
        {
            adminInfo.NotNull("用户信息不能为空");
            var currentIP = WebHttpContext.RealIP;
            await _adminDetailRepository.UpdateAsync(new
            {
                FlastLoginTime = DateTimeUtil.Now,
                FLastLoginIP = currentIP,
                FLastModifyTime = DateTimeUtil.Now,
                FLastModifyUserID = adminInfo.FID
            }, m => m.FIsDeleted == false && m.FAdminID == adminInfo.FID);
        }
    }
}