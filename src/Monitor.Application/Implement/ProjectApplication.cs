using JQCore;
using JQCore.Extensions;
using JQCore.Result;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;
using static Monitor.Constant.LockKeyConstant;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：ProjectApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：项目信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class ProjectApplication : IProjectApplication
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectDomainService _projectDomainService;
        private readonly IOperateLogDomainService _operateLogDomainService;
        private readonly IProjectCache _projectCache;

        public ProjectApplication(IProjectRepository projectRepository, IProjectDomainService projectDomainService, IProjectCache projectCache, IOperateLogDomainService operateLogDomainService)
        {
            _projectRepository = projectRepository;
            _projectDomainService = projectDomainService;
            _operateLogDomainService = operateLogDomainService;
            _projectCache = projectCache;
        }

        /// <summary>
        /// 分页获取项目列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>项目列表</returns>
        public Task<OperateResult<IPageResult<ProjectListDto>>> GetProjectListAsync(ProjectPageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _projectRepository.GetProjectListAsync(queryWhere);
            }, callMemberName: "ProjectApplication-GetProjectListAsync");
        }

        /// <summary>
        /// 同步项目信息
        /// </summary>
        /// <returns>操作结果</returns>
        public Task<OperateResult> SynchroProjectAsync()
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                await _projectCache.SynchroProjectAsync();
            }, callMemberName: "ProjectApplication-SynchroConfigAsync");
        }

        /// <summary>
        /// 添加项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> AddProjectAsync(ProjectModel projectModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var projectInfo = _projectDomainService.Create(projectModel);
                int id = await LockUtil.ExecuteWithLockAsync(Lock_ProjectModify, projectInfo.FName, TimeSpan.FromMinutes(2), async () =>
                {
                    await _projectDomainService.CheckAsync(projectInfo);
                    int projectID = (await _projectRepository.InsertOneAsync(projectInfo, keyName: "FID", ignoreFields: FID)).ToSafeInt32(0);
                    _operateLogDomainService.AddOperateLog(projectModel.OperateUserID.Value, OperateModule.Project, OperateModuleNode.Add, $"添加:{projectInfo.GetOperateDesc()}");
                    _projectDomainService.ProjectChanged(OperateType.Add, projectID);
                    return projectID;
                }, defaultValue: -1);
                if (id <= 0)
                {
                    throw new BizException("添加失败");
                }
            }, callMemberName: "ProjectApplication-AddProjectAsync");
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns>项目信息</returns>
        public Task<OperateResult<ProjectModel>> GetProjectModelAsync(int projectID)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _projectRepository.GetDtoAsync<ProjectModel>(m => m.FID == projectID && m.FIsDeleted == false, ignoreFields: OperaterUserID);
            }, callMemberName: "ProjectApplication-GetProjectModelAsync");
        }

        /// <summary>
        /// 修改项目信息
        /// </summary>
        /// <param name="projectModel">项目信息</param>
        /// <returns>操作结果</returns>
        public Task<OperateResult> EditProjectAsync(ProjectModel projectModel)
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                var projectInfo = _projectDomainService.Create(projectModel);
                var flag = await LockUtil.ExecuteWithLockAsync(Lock_ProjectModify, projectInfo.FName, TimeSpan.FromMinutes(2), async () =>
                {
                    await _projectDomainService.CheckAsync(projectInfo);
                    await _projectRepository.UpdateAsync(projectInfo, m => m.FID == projectInfo.FID, ignoreFields: IDAndCreate);
                    _operateLogDomainService.AddOperateLog(projectModel.OperateUserID.Value, OperateModule.SysConfig, OperateModuleNode.Edit, $"{projectInfo.GetOperateDesc()}");
                    _projectDomainService.ProjectChanged(OperateType.Modify, projectInfo.FID);
                    return true;
                }, defaultValue: false);
                if (!flag)
                {
                    throw new BizException("修改失败");
                }
            }, callMemberName: "ProjectApplication-EditProjectAsync");
        }

        /// <summary>
        /// 加载全部的项目列表
        /// </summary>
        /// <returns></returns>
        public Task<OperateResult<IEnumerable<ProjectListDto>>> LoadProjectListAsync()
        {
            return OperateUtil.ExecuteAsync(async () =>
            {
                return await _projectCache.GetProjectListAsync();
            }, callMemberName: "ProjectApplication-LoadProjectListAsync");
        }
    }
}