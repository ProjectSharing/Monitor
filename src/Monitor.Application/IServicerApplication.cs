using JQCore.Result;
using Monitor.Trans;
using System.Collections.Generic;
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

        /// <summary>
        /// 添加服务器信息
        /// </summary>
        /// <param name="servicerModel">服务器信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddServicerAsync(ServicerModel servicerModel);

        /// <summary>
        /// 获取服务器的编辑信息
        /// </summary>
        /// <param name="servicerID">服务器ID</param>
        /// <returns>服务器的编辑信息</returns>
        Task<OperateResult<ServicerModel>> GetServicerModelAsync(int servicerID);

        /// <summary>
        /// 修改服务器信息
        /// </summary>
        /// <param name="servicerModel">服务器信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> EditServicerAsync(ServicerModel servicerModel);

        /// <summary>
        /// 同步服务器信息
        /// </summary>
        /// <returns>操作结果</returns>
        Task<OperateResult> SynchroServiceAsync();

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <returns></returns>
        Task<OperateResult<IEnumerable<ServicerListDto>>> LoadServicerListAsync();
    }
}