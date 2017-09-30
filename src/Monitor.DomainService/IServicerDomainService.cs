using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IServicerDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：服务器信息领域服务接口
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public interface IServicerDomainService
    {
        /// <summary>
        /// 根据Model创建服务器信息
        /// </summary>
        /// <param name="servicerModel">Model信息</param>
        /// <returns>服务器信息</returns>
        ServicerInfo Create(ServicerModel servicerModel);

        /// <summary>
        /// 校验服务器信息
        /// </summary>
        /// <param name="servicerInfo">服务器信息</param>
        /// <returns></returns>
        Task CheckAsync(ServicerInfo servicerInfo);
    }
}