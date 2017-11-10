using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IWarningLogRepository.cs
    /// 接口属性：公共
    /// 类功能描述：日志预警信息数据访问接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IWarningLogRepository : IBaseDataRepository<WarningLogInfo>
    {
        /// <summary>
        /// 异步查找警告日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>警告日志列表</returns>
        Task<IPageResult<WarningLogListDto>> GetWarningLogListAsync(WarningLogPageQueryWhere queryWhere);

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WarningLogDealModel> GetWarningLogDealModel(int id);
    }
}