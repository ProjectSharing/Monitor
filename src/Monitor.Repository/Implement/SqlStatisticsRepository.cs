using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：SqlStatisticsRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL统计数据访问类
    /// 创建标识：template 2017-12-02 13:19:31
    /// </summary>
    public sealed class SqlStatisticsRepository : BaseDataRepository<SqlStatisticsInfo>, ISqlStatisticsRepository
    {
        public SqlStatisticsRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_SQLSTATISTICS, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}