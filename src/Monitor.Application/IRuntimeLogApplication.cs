using JQCore.Result;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IRuntimeLogApplication.cs
    /// 接口属性：公共
    /// 类功能描述：运行日志信息业务逻辑接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IRuntimeLogApplication
    {
        /// <summary>
        /// 新增运行日志
        /// </summary>
        /// <param name="runtimeLogModel">运行日志信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddLogAsync(RuntimeLogModel runtimeLogModel);

        /// <summary>
        /// 新增运行日志
        /// </summary>
        /// <param name="runtimeLogModelList">运行日志列表</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddLogListAsync(List<RuntimeLogModel> runtimeLogModelList);
    }
}