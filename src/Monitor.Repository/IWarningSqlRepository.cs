using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IWarningSqlRepository.cs
    /// 接口属性：公共
    /// 类功能描述：SQL预警信息数据访问接口
    /// 创建标识：template 2017-11-29 22:17:28
    /// </summary>
    public interface IWarningSqlRepository : IBaseDataRepository<WarningSqlInfo>
    {
        /// <summary>
        /// 获取预警日志列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        Task<IPageResult<WarningSqlListDto>> GetWarningSqlListAsync(WarningSqlPageQueryWhere queryWhere);

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WarningDealModel> GetWarningDealModelAsync(int id);
    }
}