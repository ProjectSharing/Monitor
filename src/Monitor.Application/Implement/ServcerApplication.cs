using Monitor.DomainService;
using Monitor.Repository;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：ServcerApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServcerApplication : IServcerApplication
    {
        private readonly IServcerRepository _servcerRepository;
        private readonly IServcerDomainService _servcerDomainService;

        public ServcerApplication(IServcerRepository servcerRepository, IServcerDomainService servcerDomainService)
        {
            _servcerRepository = servcerRepository;
            _servcerDomainService = servcerDomainService;
        }
    }
}