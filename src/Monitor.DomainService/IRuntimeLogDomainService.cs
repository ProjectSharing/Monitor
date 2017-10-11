using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IRuntimeLogDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：运行日志信息领域服务接口
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public interface IRuntimeLogDomainService
    {
        /// <summary>
        /// 创建运行日志信息
        /// </summary>
        /// <param name="runtimeLogModel">运行日志</param>
        /// <returns>运行日志信息</returns>
        Task<RuntimeLogInfo> CreateAsync(RuntimeLogModel runtimeLogModel);

        /// <summary>
        /// 分析日志
        /// </summary>
        /// <param name="runtimeLogInfo">运行日志</param>
        /// <returns></returns>
        Task AnalysisLogAsync(RuntimeLogInfo runtimeLogInfo);
    }
}