using Monitor.Domain;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IWarningSqlDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：SQL预警信息领域服务接口
    /// 创建标识：template 2017-11-29 22:17:29
    /// </summary>
    public interface IWarningSqlDomainService
    {
        /// <summary>
        /// 创建sql的警告信息
        /// </summary>
        /// <param name="runtimeSqlInfo"></param>
        /// <returns></returns>
        WarningSqlInfo Create(RuntimeSqlInfo runtimeSqlInfo);
    }
}