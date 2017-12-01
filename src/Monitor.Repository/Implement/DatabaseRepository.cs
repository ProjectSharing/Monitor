using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using JQCore.DataAccess.Utils;
using JQCore.Result;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;
using static Monitor.Constant.SqlConstant;
using static Monitor.Constant.TableConstant;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：DatabaseRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：数据库信息数据访问类
    /// 创建标识：template 2017-12-01 13:29:26
    /// </summary>
    public sealed class DatabaseRepository : BaseDataRepository<DatabaseInfo>, IDatabaseRepository
    {
        public DatabaseRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_DATABASE, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 分页获取数据库列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        public Task<IPageResult<DatabaseListDto>> GetDbListAsync(DatabasePageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("FIsDeleted=0", ReaderDataType);
            whereBuilder.AppendLike("FName", queryWhere.Name, paramKey: nameof(queryWhere.Name));

            const string selectTable = TABLE_NAME_DATABASE + SQLSERVER_WITHNOLOCK;
            const string selectColumn = "FID,FName,FDbType,FComment,ISNULL(FLastModifyTime,FCreateTime) AS FLastModifyTime";
            const string order = "ISNULL(FLastModifyTime,FCreateTime) DESC";
            return QueryPageListAsync<DatabaseListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}