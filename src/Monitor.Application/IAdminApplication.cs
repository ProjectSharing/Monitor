using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IAdminApplication.cs
    /// 接口属性：公共
    /// 类功能描述：管理员业务逻辑接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IAdminApplication
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">用户登录信息</param>
        /// <returns>登陆成功则返回用户ID</returns>
        Task<OperateResult<int>> LoginAsync(LoginModel model);
    }
}