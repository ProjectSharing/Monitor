using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：SqlParameterRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL参数信息数据访问类
    /// 创建标识：template 2017-11-29 22:17:27
    /// </summary>
    public sealed class SqlParameterRepository : BaseDataRepository<SqlParameterInfo>, ISqlParameterRepository
    {
        public SqlParameterRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_SQLPARAMETER, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}