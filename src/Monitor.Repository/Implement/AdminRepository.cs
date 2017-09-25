using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：AdminRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员数据访问类
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminRepository : BaseDataRepository<AdminInfo>, IAdminRepository
    {
        public AdminRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_ADMIN, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}