using Monitor.Repository;
using Monitor.DomainService;

namespace Monitor.Application.Implement
{
	/// <summary>
	/// 类名：SqlStatisticsApplication.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：SQL统计业务逻辑
	/// 创建标识：template 2017-12-02 13:19:32
	/// </summary>
	public sealed class SqlStatisticsApplication : ISqlStatisticsApplication
	{
		private readonly ISqlStatisticsRepository _sqlStatisticsRepository;
		private readonly ISqlStatisticsDomainService _sqlStatisticsDomainService;

		public SqlStatisticsApplication(ISqlStatisticsRepository sqlStatisticsRepository, ISqlStatisticsDomainService sqlStatisticsDomainService)
		{
			_sqlStatisticsRepository= sqlStatisticsRepository;
			_sqlStatisticsDomainService =sqlStatisticsDomainService; 
		}
	}
}
