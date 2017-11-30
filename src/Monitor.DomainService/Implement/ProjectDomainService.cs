using JQCore;
using JQCore.Extensions;
using JQCore.Hangfire;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using Monitor.Trans;
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
        /// 创建项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>项目信息</returns>
        public ProjectInfo Create(ProjectModel projectModel)
        {
            projectModel.NotNull("项目信息不能为空");
            var projectInfo = new ProjectInfo
            {
                FComment = projectModel.FComment,
                FIsDeleted = false,
                FName = projectModel.FName,
                FID = projectModel.FID ?? 0,
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = projectModel.OperateUserID.Value
            };
            if (projectInfo.FID > 0)
            {
                projectInfo.FLastModifyTime = DateTimeUtil.Now;
                projectInfo.FLastModifyUserID = projectModel.OperateUserID.Value;
            }
            return projectInfo;
        }

        /// <summary>
        /// 根据名字获取项目ID，不存在时则添加
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns></returns>
        public async Task<int> GetProjectIDAsync(string projectName)
        {
            if (projectName.IsNotNullAndNotEmptyWhiteSpace())
            {
                var projectInfo = await _projectCache.GetProjectInfoAsync(projectName.Trim());
                if (projectInfo == null)
                {
                    projectInfo = await AddWhenNotExistAsync(projectName.Trim());
                }
                if (projectInfo != null)
                {
                    return projectInfo.FID;
                }
            }
            return 0;
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
                     ProjectChanged(OperateType.Add, projectID);
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
        public void ProjectChanged(OperateType operateType, int projectID)
        {
            //TODO 更新项目缓存
            TaskScheldulingUtil.BackGroundJob(() => _projectCache.ProjectModifyAsync(projectID));
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="projectInfo">项目信息</param>
        /// <returns></returns>
        public async Task CheckAsync(ProjectInfo projectInfo)
        {
            projectInfo.NotNull("项目信息不能为空");
            //判断是否有存在相同的Mac地址
            var existNameInfo = await _projectRepository.GetInfoAsync(m => m.FName == projectInfo.FName && m.FIsDeleted == false);
            if (existNameInfo != null)
            {
                if (existNameInfo.FID != projectInfo.FID)
                {
                    throw new BizException("名字已存在");
                }
            }
        }
    }
}