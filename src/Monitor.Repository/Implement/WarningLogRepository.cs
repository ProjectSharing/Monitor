using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：WarningLogRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志预警信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningLogRepository : BaseDataRepository<WarningLogInfo>, IWarningLogRepository
    {
        public WarningLogRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_WARNINGLOG, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}