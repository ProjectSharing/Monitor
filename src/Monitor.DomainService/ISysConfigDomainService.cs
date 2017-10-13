using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：ISysConfigDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：系统配置信息领域服务接口
    /// 创建标识：template 2017-10-10 13:32:46
    /// </summary>
    public interface ISysConfigDomainService
    {
        /// <summary>
        /// 创建配置信息
        /// </summary>
        /// <param name="sysConfigModel">配置信息</param>
        /// <returns>配置信息</returns>
        SysConfigInfo Create(SysConfigModel sysConfigModel);

        /// <summary>
        /// 校验服务器信息
        /// </summary>
        /// <param name="servicerInfo">服务器信息</param>
        /// <returns></returns>
        Task CheckAsync(SysConfigInfo sysConfigInfo);

        /// <summary>
        /// 配置更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="servicerID">配置ID</param>
        /// <returns></returns>
        Task ConfigChangedAsync(OperateType operateType, int configID);
    }
}