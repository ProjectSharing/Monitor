using Monitor.Domain;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IAdminDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：管理员领域服务接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IAdminDomainService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户信息</returns>
        Task<AdminInfo> LoginAsync(string userName, string pwd);

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        /// <param name="pwd">输入的密码</param>
        void LoginCheck(AdminInfo adminInfo, string pwd);

        /// <summary>
        /// 校验是否可以登录
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        void CheckCanLogin(AdminInfo adminInfo);

        /// <summary>
        /// 校验用户是否可用
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        void UserIsEnable(AdminInfo adminInfo);

        /// <summary>
        /// 校验用户密码
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="errorMessage">错误提示信息</param>
        void CheckPwd(AdminInfo adminInfo, string pwd, string errorMessage = null);
    }
}