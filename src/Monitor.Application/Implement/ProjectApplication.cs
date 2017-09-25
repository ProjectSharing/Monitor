using Monitor.DomainService;
using Monitor.Repository;

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

        public ProjectApplication(IProjectRepository projectRepository, IProjectDomainService projectDomainService)
        {
            _projectRepository = projectRepository;
            _projectDomainService = projectDomainService;
        }
    }
}