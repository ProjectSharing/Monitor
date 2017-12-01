using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IDatabaseDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：数据库信息领域服务接口
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public interface IDatabaseDomainService
    {
        /// <summary>
        /// 创建数据库信息
        /// </summary>
        /// <param name="databaseMode">数据库信息</param>
        /// <returns>数据库信息</returns>
        DatabaseInfo Create(DatabaseModel databaseMode);

        /// <summary>
        /// 根据数据库名字、类型获取数据库ID
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库ID</returns>
        Task<int> GetDatabaseIDAsync(string dbName, string dbType);

        /// <summary>
        /// 添加数据库信息,当数据库信息不存在时
        /// </summary>
        /// <param name="dbName">数据库名字</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库信息</returns>
        Task<DatabaseInfo> AddWhenNotExistAsync(string dbName, string dbType);

        /// <summary>
        /// 数据库更新
        /// </summary>
        /// <param name="operateType">操作类型</param>
        /// <param name="dbID">数据库ID</param>
        void DatabaseChanged(OperateType operateType, int dbID);

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="databaseInfo">数据库信息</param>
        /// <returns></returns>
        Task CheckAsync(DatabaseInfo databaseInfo);
    }
}