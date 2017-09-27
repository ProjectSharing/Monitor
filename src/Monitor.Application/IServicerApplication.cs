using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IServicerApplication.cs
    /// 接口属性：公共
    /// 类功能描述：服务器信息业务逻辑接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IServicerApplication
    {
        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>服务器列表</returns>
        Task<OperateResult<IPageResult<ServicerListDto>>> GetServiceListAsync(ServicePageQueryWhere queryWhere);
    }
}