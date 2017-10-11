using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Domain;
using Monitor.Constant;

namespace Monitor.Repository.Implement
{
	/// <summary>
	/// 类名：SysConfigRepository.cs
	/// 类属性：公共类（非静态）
	/// 类功能描述：系统配置信息数据访问类
	/// 创建标识：template 2017-10-10 13:32:46
	/// </summary>
	public sealed class SysConfigRepository : BaseDataRepository<SysConfigInfo>, ISysConfigRepository
	{
		public SysConfigRepository(IDataAccessFactory dataAccessFactory): base(dataAccessFactory,TableConstant.TABLE_NAME_SYSCONFIG, DbConnConstant.Conn_Name_Monitor)
		{
		}
	}
}
