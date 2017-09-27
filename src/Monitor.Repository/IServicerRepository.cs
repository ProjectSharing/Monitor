using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IServicerRepository.cs
    /// 接口属性：公共
    /// 类功能描述：服务器信息数据访问接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IServicerRepository : IBaseDataRepository<ServicerInfo>
    {
        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>服务器列表</returns>
        Task<IPageResult<ServicerListDto>> GetServiceListAsync(ServicePageQueryWhere queryWhere);
    }
}