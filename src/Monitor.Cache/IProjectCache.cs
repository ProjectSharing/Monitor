using Monitor.Domain;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IProjectCache.cs
    /// 接口属性：公共
    /// 类功能描述：IProjectCache接口
    /// 创建标识：yjq 2017/10/3 19:29:58
    /// </summary>
    public interface IProjectCache
    {
        /// <summary>
        /// 异步根据名字或缓存中获取项目信息
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        Task<ProjectInfo> GetProjectInfoAsync(string projectName);

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns>项目列表</returns>
        Task<IEnumerable<ProjectListDto>> GetProjectListAsync();

        /// <summary>
        /// 项目信息发生变化
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        Task ProjectModifyAsync(int projectID);

        /// <summary>
        /// 同步项目信息
        /// </summary>
        /// <returns></returns>
        Task SynchroProjectAsync();
    }
}