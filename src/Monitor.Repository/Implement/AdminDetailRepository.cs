using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using Monitor.Constant;
using Monitor.Domain;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：AdminDetailRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：管理员详情信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public sealed class AdminDetailRepository : BaseDataRepository<AdminDetailInfo>, IAdminDetailRepository
    {
        public AdminDetailRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_ADMINDETAIL, DbConnConstant.Conn_Name_Monitor)
        {
        }
    }
}