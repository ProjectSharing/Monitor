using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IProjectDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：项目信息领域服务接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IProjectDomainService
    {
        /// <summary>
        /// 根据名字获取项目ID，不存在时则添加
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns></returns>
        Task<int> GetProjectIDAsync(string projectName);

        /// <summary>
        /// 创建项目，当名字不存在时
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        Task<ProjectInfo> AddWhenNotExistAsync(string projectName);

        /// <summary>
        /// 项目更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        void ProjectChanged(OperateType operateType, int projectID);

        /// <summary>
        /// 创建项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>项目信息</returns>
        ProjectInfo Create(ProjectModel projectModel);

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="projectInfo">项目信息</param>
        /// <returns></returns>
        Task CheckAsync(ProjectInfo projectInfo);
    }
}