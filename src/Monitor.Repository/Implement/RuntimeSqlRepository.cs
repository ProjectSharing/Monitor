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
    /// 类名：RuntimeSqlRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：执行SQL信息数据访问类
    /// 创建标识：template 2017-11-29 22:17:30
    /// </summary>
    public sealed class RuntimeSqlRepository : BaseDataRepository<RuntimeSqlInfo>, IRuntimeSqlRepository
    {
        public RuntimeSqlRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TABLE_NAME_RUNTIMESQL, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 加载sql运行列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public Task<IPageResult<RuntimeSqlListDto>> GetRuntimSqlListAsync(RuntimeSqlPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("A.FIsDeleted=0", ReaderDataType);
            whereBuilder.AppendEqual("A.FProjectID", queryWhere.ProjectID, nameof(queryWhere.ProjectID))
                        .AppendEqual("A.FServicerID", queryWhere.ServicerID, nameof(queryWhere.ServicerID))
                        .AppendEqual("A.FSource", queryWhere.Source, nameof(queryWhere.Source))
                        .AppendEqual("A.FRequestGuid", queryWhere.RequestGuid, nameof(queryWhere.RequestGuid))
                        .AppendMoreThanOrEqual("A.FTimeElapsed", queryWhere.MinTimeElapsed, nameof(queryWhere.MinTimeElapsed))
                        .AppendLessThan("A.FTimeElapsed", queryWhere.MaxTimeElapsed, nameof(queryWhere.MaxTimeElapsed))
                        .AppendMoreThanOrEqual("A.FExecutedTime", queryWhere.ExecuteTimeStart, nameof(queryWhere.ExecuteTimeStart))
                        .AppendLessThan("A.FExecuteTime", queryWhere.ExecuteTimeEndValue, nameof(queryWhere.ExecuteTimeEndValue))
                        ;
            string selectTable = $"{TABLE_NAME_RUNTIMESQL} A {SQLSERVER_WITHNOLOCK} LEFT JOIN {TABLE_NAME_SERVCER} B {SQLSERVER_WITHNOLOCK} ON A.FServicerID=B.FID AND B.FIsDeleted=0";
            const string selectColumn = "A.FID,A.FProjectName,B.FName AS FServicerName,A.FSqlDbType,A.FSqlText,A.FMemberName,A.FRequestGuid,A.FTimeElapsed,A.FSource,A.FIsSuccess,A.FExecutedTime";
            const string order = "A.FExecutedTime DESC";
            return QueryPageListAsync<RuntimeSqlListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}