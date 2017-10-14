using System.Threading.Tasks;

namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ISysConfigCache.cs
    /// 接口属性：公共
    /// 类功能描述：ISysConfigCache接口
    /// 创建标识：yjq 2017/10/10 13:35:41
    /// </summary>
    public interface ISysConfigCache
    {
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置的Key</param>
        /// <returns>配置值</returns>
        Task<string> GetValueAsync(string key);

        /// <summary>
        /// 修改配置文件到redis缓存
        /// </summary>
        /// <param name="configID">配置ID</param>
        /// <returns></returns>
        Task SysConfigModifyAsync(int configID);

        /// <summary>
        /// 同步系统配置
        /// </summary>
        /// <returns></returns>
        Task SynchroConfigAsync();
    }
}