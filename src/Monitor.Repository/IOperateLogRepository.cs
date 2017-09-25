using JQCore.DataAccess.Repositories;
using Monitor.Domain;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IOperateLogRepository.cs
    /// 接口属性：公共
    /// 类功能描述：管理员操作记录数据访问接口
    /// 创建标识：template 2017-09-24 11:55:19
    /// </summary>
    public interface IOperateLogRepository : IBaseDataRepository<OperateLogInfo>
    {
    }
}