using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：SqlParameterApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：SQL参数信息业务逻辑
	/// 创建标识：template 2017-11-29 22:17:28
	/// </summary>
	public sealed class SqlParameterApplication : ISqlParameterApplication
	{
		private readonly ISqlParameterRepository _sqlParameterRepository;
		private readonly ISqlParameterDomainService _sqlParameterDomainService;

		public SqlParameterApplication(ISqlParameterRepository sqlParameterRepository, ISqlParameterDomainService sqlParameterDomainService)
		{
			_sqlParameterRepository= sqlParameterRepository;
			_sqlParameterDomainService =sqlParameterDomainService; 
		}
	}
}
