using Monitor.Constant;
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

        /// <summary>
        /// 创建服务器，当名字不存在时
        /// </summary>
        /// <param name="servicerMac">Mac地址</param>
        /// <returns>项目信息</returns>
        Task<ServicerInfo> AddWhenNotExistAsync(string servicerMac);

        /// <summary>
        /// 服务器更改
        /// </summary>
        /// <param name="operateType">更改类型</param>
        /// <param name="servicerID">服务器ID</param>
        /// <returns></returns>
        void ServicerChanged(OperateType operateType, int servicerID);
    }
}