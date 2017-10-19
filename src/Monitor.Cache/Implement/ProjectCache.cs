using JQCore.Redis;
using JQCore.Utils;
using Monitor.Domain;
using Monitor.Repository;
using System.Linq;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;
using JQCore.Extensions;

namespace Monitor.Cache.Implement
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ProjectCache.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/10/3 19:30:30
    /// </summary>
    public sealed class ProjectCache : RedisBaseRepository, IProjectCache
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectCache(IProjectRepository projectRepository, IRedisDatabaseProvider databaseProvider) : base(databaseProvider, RedisOptionProvider.DefaultOption)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// 根据名字或缓存中获取项目信息
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        public Task<ProjectInfo> GetProjectInfoAsync(string projectName)
        {
            return GetValueAsync(async () =>
             {
                 var projectList = await RedisClient.HashValuesAsync<ProjectInfo>(Cache_ProjectList);
                 return projectList.FirstOrDefault(m => m.FName == projectName && m.FIsDeleted == false);
             }, async () =>
             {
                 return await _projectRepository.GetInfoAsync(m => m.FName == projectName);
             }, memberName: "ProjectCache-GetProjectInfoAsync");
        }

        /// <summary>
        /// 项目信息发生变化
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        public Task ProjectModifyAsync(int projectID)
        {
            return SetValueAsync(async () =>
            {
                var projectInfo = await _projectRepository.GetInfoAsync(m => m.FID == projectID && m.FIsDeleted == false);
                if (projectInfo != null)
                {
                    await RedisClient.HashSetAsync(Cache_ProjectList, projectID.ToString(), projectInfo);
                }
                else
                {
                    if (await RedisClient.HashExistsAsync(Cache_ProjectList, projectID.ToString()))
                    {
                        await RedisClient.HashDeleteAsync(Cache_ProjectList, projectID.ToString());
                    }
                }
            }, memberName: "ProjectCache-ProjectModifyAsync");
        }

        /// <summary>
        /// 同步项目信息
        /// </summary>
        /// <returns></returns>
        public Task SynchroProjectAsync()
        {
            return SetValueAsync(async () =>
            {
                LogUtil.Info("开始同步项目信息");
                var lastSynchroTime = await GetLastSynchroTimeAsync(Cache_Synchro_Project);
                LogUtil.Info($"项目信息上次同步时间;{lastSynchroTime.ToDefaultFormat()}");
                var projectList = await _projectRepository.QueryListAsync(m => m.FCreateTime >= lastSynchroTime || m.FLastModifyTime >= lastSynchroTime);
                LogUtil.Info($"共{(projectList?.Count().ToString()) ?? "0"}条信息需要同步");
                foreach (var projectInfo in projectList)
                {
                    if (!projectInfo.FIsDeleted)
                    {
                        await RedisClient.HashSetAsync(Cache_ProjectList, projectInfo.FID.ToString(), projectInfo);
                    }
                    else
                    {
                        if (await RedisClient.HashExistsAsync(Cache_ProjectList, projectInfo.FID.ToString()))
                        {
                            await RedisClient.HashDeleteAsync(Cache_ProjectList, projectInfo.FID.ToString());
                        }
                    }
                    LogUtil.Info($"项目信息【{projectInfo.FID.ToString()}】同步成功");
                }
                await UpdateLastSynchroTimeAsync(Cache_Synchro_Project);
                LogUtil.Info("同步项目信息完成");
            }, memberName: "ProjectCache-SynchroConfigAsync");
        }
    }
}