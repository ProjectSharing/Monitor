using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IProjectApplication.cs
    /// 接口属性：公共
    /// 类功能描述：项目信息业务逻辑接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IProjectApplication
    {
        /// <summary>
        /// 分页获取项目列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>项目列表</returns>
        Task<OperateResult<IPageResult<ProjectListDto>>> GetProjectListAsync(ProjectPageQueryWhere queryWhere);

        /// <summary>
        /// 同步项目信息
        /// </summary>
        /// <returns>操作结果</returns>
        Task<OperateResult> SynchroProjectAsync();

        /// <summary>
        /// 添加项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddProjectAsync(ProjectModel projectModel);

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns>项目信息</returns>
        Task<OperateResult<ProjectModel>> GetProjectModelAsync(int projectID);

        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> EditProjectAsync(ProjectModel projectModel);
    }
}