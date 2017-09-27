using JQCore.Result;
using Monitor.DomainService;
using Monitor.Repository;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application.Implement
{
    /// <summary>
    /// 类名：ServicerApplication.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：服务器信息业务逻辑
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class ServicerApplication : IServicerApplication
    {
        private readonly IServicerRepository _servcerRepository;
        private readonly IServicerDomainService _servcerDomainService;

        public ServicerApplication(IServicerRepository servcerRepository, IServicerDomainService servcerDomainService)
        {
            _servcerRepository = servcerRepository;
            _servcerDomainService = servcerDomainService;
        }

        /// <summary>
        /// 分页获取服务器列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>服务器列表</returns>
        public Task<OperateResult<IPageResult<ServicerListDto>>> GetServiceListAsync(ServicePageQueryWhere queryWhere)
        {
            return OperateUtil.ExecuteAsync(() =>
            {
                return _servcerRepository.GetServiceListAsync(queryWhere);
            }, callMemberName: "ServicerApplication-GetServiceListAsync");
        }
    }
}