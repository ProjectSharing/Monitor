using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：WarningInfoOperateLogRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：预警信息操作记录数据访问类
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningInfoOperateLogRepository : BaseDataRepository<WarningInfoOperateLogInfo>, IWarningInfoOperateLogRepository
    {
        public WarningInfoOperateLogRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_WARNINGINFOOPERATELOG, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}