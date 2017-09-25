using JQCore.DataAccess.Repositories;
using Monitor.Domain;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IWarningLogRepository.cs
    /// 接口属性：公共
    /// 类功能描述：日志预警信息数据访问接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IWarningLogRepository : IBaseDataRepository<WarningLogInfo>
    {
    }
}