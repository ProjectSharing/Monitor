using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IRuntimeSqlRepository.cs
    /// 接口属性：公共
    /// 类功能描述：执行SQL信息数据访问接口
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public interface IRuntimeSqlRepository : IBaseDataRepository<RuntimeSqlInfo>
    {
        /// <summary>
        /// 加载sql运行列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        Task<IPageResult<RuntimeSqlListDto>> GetRuntimSqlListAsync(RuntimeSqlPageQueryWhere queryWhere);
    }
}