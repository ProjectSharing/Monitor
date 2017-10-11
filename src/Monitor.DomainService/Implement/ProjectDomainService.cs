using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using System;
using System.Threading.Tasks;
using static Monitor.Constant.LockKeyConstant;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：ProjectDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：项目信息业务逻辑处理
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class ProjectDomainService : IProjectDomainService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectCache _projectCache;

        public ProjectDomainService(IProjectCache projectCache, IProjectRepository projectRepository)
        {
            _projectCache = projectCache;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// 创建项目，当名字不存在时
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        public Task<ProjectInfo> AddWhenNotExistAsync(string projectName)
        {
            projectName.NotNull("项目名称不能为空");
            var projectInfo = new ProjectInfo
            {
                FCreateTime = DateTimeUtil.Now,
                FIsDeleted = false,
                FName = projectName,
                FCreateUserID = -1
            };
            return LockUtil.ExecuteWithLockAsync(Lock_ProjectModify, projectName, TimeSpan.FromMinutes(2), async () =>
             {
                 var existProjectInfo = await _projectRepository.GetInfoAsync(m => m.FIsDeleted == false && m.FName == projectName);
                 if (existProjectInfo != null)
                 {
                     return existProjectInfo;
                 }
                 else
                 {
                     int projectID = (await _projectRepository.InsertOneAsync(projectInfo, keyName: "FID", ignoreFields: IgnoreConstant.FID)).ToSafeInt32(0);
                     projectInfo.FID = projectID;
                     await ProjectChangedAsync(OperateType.Add, projectID);
                     return projectInfo;
                 }
             });
        }

        /// <summary>
        /// 项目更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        public Task ProjectChangedAsync(OperateType operateType, int projectID)
        {
            //TODO 更新项目缓存
            return Task.Delay(1);
        }
    }
}