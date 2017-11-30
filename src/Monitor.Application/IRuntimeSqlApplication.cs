using JQCore.Result;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IRuntimeSqlApplication.cs
    /// 接口属性：公共
    /// 类功能描述：执行SQL信息业务逻辑接口
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public interface IRuntimeSqlApplication
    {
        /// <summary>
        /// 添加运行sql信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddRuntimeSqlAsync(RuntimeSqlModel runtimeSqlModel);

        /// <summary>
        /// 新增运行sql信息
        /// </summary>
        /// <param name="runtimeLogModelList"></param>
        /// <returns>操作结果</returns>
        OperateResult AddRuntimeSqlList(List<RuntimeSqlModel> runtimeSqlModelList);

        /// <summary>
        /// 加载sql运行列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        Task<OperateResult<IPageResult<RuntimeSqlListDto>>> GetRuntimSqlListAsync(RuntimeSqlPageQueryWhere queryWhere);
    }
}