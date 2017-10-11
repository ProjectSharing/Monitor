using Monitor.Domain;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IWarningLogDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：日志预警信息领域服务接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IWarningLogDomainService
    {
        /// <summary>
        /// 创建警告提示信息
        /// </summary>
        /// <param name="runtimeLogInfo">运行日志信息</param>
        /// <returns>警告提示信息</returns>
        WarningLogInfo Create(RuntimeLogInfo runtimeLogInfo);
    }
}