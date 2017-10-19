using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IProjectRepository.cs
    /// 接口属性：公共
    /// 类功能描述：项目信息数据访问接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IProjectRepository : IBaseDataRepository<ProjectInfo>
    {
        /// <summary>
        /// 分页获取项目列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>项目列表</returns>
        Task<IPageResult<ProjectListDto>> GetProjectListAsync(ProjectPageQueryWhere queryWhere);
    }
}