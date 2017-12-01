using Monitor.Domain;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Cache
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：IDatabaseCache.cs
    /// 接口属性：公共
    /// 类功能描述：IDatabaseCache接口
    /// 创建标识：yjq 2017/12/1 13:57:36
    /// </summary>
    public interface IDatabaseCache
    {
        /// <summary>
        /// 根据名字或缓存中获取项目信息
        /// </summary>
        /// <param name="projectName">项目名字</param>
        /// <returns>项目信息</returns>
        Task<DatabaseInfo> GetDatabaseInfoAsync(string databaseName, string dbType);

        /// <summary>
        /// 获取当前所有数据库列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DatabaseListDto>> GetDatabaseListAsync();

        /// <summary>
        /// 数据库信息发生变化
        /// </summary>
        /// <param name="databaseID">数据库ID</param>
        /// <returns></returns>
        Task DatabaseModifyAsync(int databaseID);

        /// <summary>
        /// 同步数据库信息
        /// </summary>
        /// <returns></returns>
        Task SynchroDatabaseAsync();
    }
}