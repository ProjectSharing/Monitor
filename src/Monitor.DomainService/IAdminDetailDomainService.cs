using Monitor.Domain;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IAdminDetailDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：管理员详情信息领域服务接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IAdminDetailDomainService
    {
        /// <summary>
        /// 更新用户上次登录的信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        Task ChangeLastLoginInfoAsync(AdminInfo adminInfo);
    }
}