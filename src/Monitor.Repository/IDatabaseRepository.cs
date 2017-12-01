using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IDatabaseRepository.cs
    /// 接口属性：公共
    /// 类功能描述：数据库信息数据访问接口
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public interface IDatabaseRepository : IBaseDataRepository<DatabaseInfo>
    {
        /// <summary>
        /// 分页获取数据库列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        Task<IPageResult<DatabaseListDto>> GetDbListAsync(DatabasePageQueryWhere queryWhere);
    }
}