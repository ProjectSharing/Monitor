using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：ServiceGroupMapApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：服务器所属组关系业务逻辑
	/// 创建标识：template 2017-09-27 16:49:37
	/// </summary>
	public sealed class ServiceGroupMapApplication : IServiceGroupMapApplication
	{
		private readonly IServiceGroupMapRepository _serviceGroupMapRepository;
		private readonly IServiceGroupMapDomainService _serviceGroupMapDomainService;

		public ServiceGroupMapApplication(IServiceGroupMapRepository serviceGroupMapRepository, IServiceGroupMapDomainService serviceGroupMapDomainService)
		{
			_serviceGroupMapRepository= serviceGroupMapRepository;
			_serviceGroupMapDomainService =serviceGroupMapDomainService; 
		}
	}
}
