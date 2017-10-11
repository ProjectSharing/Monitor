using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：SysConfigApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：系统配置信息业务逻辑
	/// 创建标识：template 2017-10-10 13:32:46
	/// </summary>
	public sealed class SysConfigApplication : ISysConfigApplication
	{
		private readonly ISysConfigRepository _sysConfigRepository;
		private readonly ISysConfigDomainService _sysConfigDomainService;

		public SysConfigApplication(ISysConfigRepository sysConfigRepository, ISysConfigDomainService sysConfigDomainService)
		{
			_sysConfigRepository= sysConfigRepository;
			_sysConfigDomainService =sysConfigDomainService; 
		}
	}
}
