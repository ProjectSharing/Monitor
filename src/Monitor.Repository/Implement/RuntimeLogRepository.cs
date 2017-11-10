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
    /// 类名：RuntimeLogRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：运行日志信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:20
    /// </summary>
    public sealed class RuntimeLogRepository : BaseDataRepository<RuntimeLogInfo>, IRuntimeLogRepository
    {
        public RuntimeLogRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TABLE_NAME_RUNTIMELOG, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 加载运行时日志信息
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public Task<IPageResult<RuntimeLogListDto>> GetRuntimeLogListAsync(RuntimeLogPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("A.FIsDeleted=0", ReaderDataType);
            whereBuilder.AppendEqual("A.FLogLevel", queryWhere.LogLevel, nameof(queryWhere.LogLevel))
                        .AppendEqual("A.FProjectID", queryWhere.ProjectID, nameof(queryWhere.ProjectID))
                        .AppendEqual("A.FServicerID", queryWhere.ServicerID, nameof(queryWhere.ServicerID))
                        .AppendEqual("A.FSource", queryWhere.Source, nameof(queryWhere.Source))
                        .AppendEqual("A.FRequestGuid", queryWhere.RequestGuid, nameof(queryWhere.RequestGuid))
                        .AppendMoreThanOrEqual("A.FExecuteTime", queryWhere.ExecuteTimeStart, nameof(queryWhere.ExecuteTimeStart))
                        .AppendLessThan("A.FExecuteTime", queryWhere.ExecuteTimeEndValue, nameof(queryWhere.ExecuteTimeEndValue))
                        ;

            string selectTable = $"{TABLE_NAME_RUNTIMELOG} A {SQLSERVER_WITHNOLOCK} LEFT JOIN {TABLE_NAME_SERVCER} B {SQLSERVER_WITHNOLOCK} ON A.FServicerID=B.FID AND B.FIsDeleted=0";
            const string selectColumn = "A.FID,A.FLogLevel,A.FProjectName,B.FName AS FServicerName,A.FCallMemberName,A.FContent,A.FSource,A.FRequestGuid,A.FExecuteTime,A.FCreateTime";
            const string order = "A.FExecuteTime DESC";
            return QueryPageListAsync<RuntimeLogListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}