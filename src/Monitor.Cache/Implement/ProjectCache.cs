using JQCore.Redis;
using Monitor.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Monitor.Constant.CacheKeyConstant;
using System.Linq;
using Monitor.Repository;

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
             }, memberName: "ProjectCache-GetProjectInfo");
        }
    }
}
