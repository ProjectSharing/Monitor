using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Domain;
using Monitor.Constant;

namespace Monitor.Repository.Implement
{
	/// <summary>
	/// 类名：ServiceGroupMapRepository.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：服务器所属组关系数据访问类
	/// 创建标识：template 2017-09-27 16:49:37
	/// </summary>
	public sealed class ServiceGroupMapRepository : BaseDataRepository<ServiceGroupMapInfo>, IServiceGroupMapRepository
	{
		public ServiceGroupMapRepository(IDataAccessFactory dataAccessFactory): base(dataAccessFactory,TableConstant.TABLE_NAME_SERVICEGROUPMAP, DbConnConstant.Conn_Name_Monitor)
		{
		}
	}
}
