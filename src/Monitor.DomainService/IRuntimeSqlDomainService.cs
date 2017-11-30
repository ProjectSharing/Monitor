using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IRuntimeSqlDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：执行SQL信息领域服务接口
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public interface IRuntimeSqlDomainService
    {
        /// <summary>
        /// 添加运行sql信息
        /// </summary>
        /// <param name="runtimeSqlModel"></param>
        /// <returns></returns>
        Task<RuntimeSqlInfo> AddRuntimeSqlAsync(RuntimeSqlModel runtimeSqlModel);

        /// <summary>
        /// 分析运行的sql信息
        /// </summary>
        /// <param name="runtimeSqlInfo"></param>
        /// <returns></returns>
        Task AnalysisRuntimeSqlAsync(RuntimeSqlInfo runtimeSqlInfo);
    }
}