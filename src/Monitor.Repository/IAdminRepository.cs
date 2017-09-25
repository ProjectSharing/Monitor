using JQCore.DataAccess.Repositories;
using Monitor.Domain;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IAdminRepository.cs
    /// 接口属性：公共
    /// 类功能描述：管理员数据访问接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IAdminRepository : IBaseDataRepository<AdminInfo>
    {
    }
}