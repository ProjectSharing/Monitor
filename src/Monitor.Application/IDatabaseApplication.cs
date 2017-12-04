using JQCore.Result;
using Monitor.Trans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IDatabaseApplication.cs
    /// 接口属性：公共
    /// 类功能描述：数据库信息业务逻辑接口
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public interface IDatabaseApplication
    {
        /// <summary>
        /// 分页获取数据库列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>数据库列表</returns>
        Task<OperateResult<IPageResult<DatabaseListDto>>> GetDbListAsync(DatabasePageQueryWhere queryWhere);

        /// <summary>
        /// 同步数据库信息
        /// </summary>
        /// <returns>操作结果</returns>
        Task<OperateResult> SynchroDatabaseAsync();

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <param name="databaseModel">数据库信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> AddDatabaseAsync(DatabaseModel databaseModel);

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="dbID">数据库ID</param>
        /// <returns>数据库信息</returns>
        Task<OperateResult<DatabaseModel>> GetDatabaseModelAsync(int dbID);

        /// <summary>
        /// 修改数据库信息
        /// </summary>
        /// <param name="databaseModel">数据库信息</param>
        /// <returns>操作结果</returns>
        Task<OperateResult> EditDatabaseAsync(DatabaseModel databaseModel);

        /// <summary>
        /// 加载全部的数据库信息
        /// </summary>
        /// <returns></returns>
        Task<OperateResult<IEnumerable<DatabaseListDto>>> LoadDbListAsync();

        /// <summary>
        /// 获取表结构列表
        /// </summary>
        /// <param name="dbID">数据库ID</param>
        /// <returns>表结构列表</returns>
        Task<OperateResult<IEnumerable<TableStructureDto>>> GetTableStructureListAsync(int dbID);
    }
}