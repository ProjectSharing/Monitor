using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：ServiceGroupApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：服务器组信息业务逻辑
	/// 创建标识：template 2017-09-27 16:35:41
	/// </summary>
	public sealed class ServiceGroupApplication : IServiceGroupApplication
	{
		private readonly IServiceGroupRepository _serviceGroupRepository;
		private readonly IServiceGroupDomainService _serviceGroupDomainService;

		public ServiceGroupApplication(IServiceGroupRepository serviceGroupRepository, IServiceGroupDomainService serviceGroupDomainService)
		{
			_serviceGroupRepository= serviceGroupRepository;
			_serviceGroupDomainService =serviceGroupDomainService; 
		}
	}
}
