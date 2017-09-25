using JQCore;
using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using System.Threading.Tasks;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：AdminDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminDomainService : IAdminDomainService
    {
        private readonly IAdminCache _adminCache;
        private readonly IAdminRepository _adminRepository;

        public AdminDomainService(IAdminRepository adminRepository, IAdminCache adminCache)
        {
            _adminCache = adminCache;
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>用户信息</returns>
        public async Task<AdminInfo> LoginAsync(string userName, string pwd)
        {
            userName.NotNullAndNotEmptyWhiteSpace("请输入用户名");
            pwd.NotNullAndNotEmptyWhiteSpace("请输入密码");
            var userInfo = await _adminRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FEmail == userName.Trim());
            LoginCheck(userInfo, pwd);
            _adminCache.ClearLoginCount(userName);
            return userInfo;
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        /// <param name="pwd">输入的密码</param>
        public void LoginCheck(AdminInfo adminInfo, string pwd)
        {
            adminInfo.NotNull("账号或密码错误");
            CheckCanLogin(adminInfo);
            CheckPwd(adminInfo, pwd);
        }

        /// <summary>
        /// 校验是否可以登录
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        public void CheckCanLogin(AdminInfo adminInfo)
        {
            UserIsEnable(adminInfo);
            //获取尝试登录的失败次数
            var tryLoginErrorCount = _adminCache.GetTryLoginCount(adminInfo.FEmail);
            if (tryLoginErrorCount >= 5)
            {
                throw new BizException("由于密码错误次数过多,您的账号已被锁定");
            }
        }

        /// <summary>
        /// 校验用户是否可用
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        public void UserIsEnable(AdminInfo adminInfo)
        {
            if (adminInfo.FState != UserState.Enable)
            {
                throw new BizException($"该用户已{adminInfo.FState.Desc()}，请联系管理员");
            }
        }

        /// <summary>
        /// 校验用户密码
        /// </summary>
        /// <param name="adminInfo">用户信息</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="errorMessage">错误提示信息</param>
        public void CheckPwd(AdminInfo adminInfo, string pwd, string errorMessage = null)
        {
            adminInfo.NotNull("用户不存在");
            string loginPwd = string.Concat(pwd, adminInfo.FPwdSalt).ToMd5();
            if (!adminInfo.FPwd.Equals(loginPwd))
            {
                throw new BizException(errorMessage ?? "用户或密码错误");
            }
        }
    }
}