using Monitor.Constant;
using Monitor.Domain;
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
        Task ProjectChangedAsync(OperateType operateType, int projectID);
    }
}