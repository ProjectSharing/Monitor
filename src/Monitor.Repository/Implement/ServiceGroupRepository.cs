using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Domain;
using Monitor.Constant;

namespace Monitor.Repository.Implement
{
	/// <summary>
	/// 类名：ServiceGroupRepository.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：服务器组信息数据访问类
	/// 创建标识：template 2017-09-27 16:35:40
	/// </summary>
	public sealed class ServiceGroupRepository : BaseDataRepository<ServiceGroupInfo>, IServiceGroupRepository
	{

		public ServiceGroupRepository(IDataAccessFactory dataAccessFactory): base(dataAccessFactory,TableConstant.TABLE_NAME_SERVICEGROUP, DbConnConstant.Conn_Name_Monitor)
		{
		}
	}
}
