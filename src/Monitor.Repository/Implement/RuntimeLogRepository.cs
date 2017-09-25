using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：RuntimeLogRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogRepository : BaseDataRepository<RuntimeLogInfo>, IRuntimeLogRepository
    {
        public RuntimeLogRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_RUNTIMELOG, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}