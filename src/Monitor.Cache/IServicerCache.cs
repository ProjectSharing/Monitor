using Monitor.Domain;
using System.Threading.Tasks;

namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IServerCache.cs
    /// 接口属性：公共
    /// 类功能描述：IServerCache接口
    /// 创建标识：yjq 2017/10/9 14:52:48
    /// </summary>
    public interface IServicerCache
    {
        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="servicerMac">服务器Mac地址</param>
        /// <returns>服务器信息</returns>
        Task<ServicerInfo> GetServicerInfoAsync(string servicerMac);

        /// <summary>
        /// 修改服务器到redis缓存
        /// </summary>
        /// <param name="serviserID">服务器ID</param>
        /// <returns></returns>
        Task ServicerModifyAsync(int serviserID);

        /// <summary>
        /// 同步服务器信息
        /// </summary>
        /// <returns></returns>
        Task SynchroServiceAsync();
    }
}