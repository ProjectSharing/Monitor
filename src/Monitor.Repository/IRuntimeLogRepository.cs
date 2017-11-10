using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IRuntimeLogRepository.cs
    /// 接口属性：公共
    /// 类功能描述：运行日志信息数据访问接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IRuntimeLogRepository : IBaseDataRepository<RuntimeLogInfo>
    {
        /// <summary>
        /// 加载运行时日志信息
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        Task<IPageResult<RuntimeLogListDto>> GetRuntimeLogListAsync(RuntimeLogPageQueryWhere queryWhere);
    }
}