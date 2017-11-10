using JQCore.DataAccess;
using JQCore.DataAccess.DbClient;
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
    /// 类名：WarningLogRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：日志预警信息数据访问类
    /// 创建标识：template 2017-09-24 11:55:21
    /// </summary>
    public sealed class WarningLogRepository : BaseDataRepository<WarningLogInfo>, IWarningLogRepository
    {
        public WarningLogRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_WARNINGLOG, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 异步查找警告日志列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>警告日志列表</returns>
        public Task<IPageResult<WarningLogListDto>> GetWarningLogListAsync(WarningLogPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("A.FIsDeleted=0", ReaderDataType);
            whereBuilder.AppendEqual("B.FProjectID", queryWhere.ProjectID, nameof(queryWhere.ProjectID))
                        .AppendEqual("B.FServicerID", queryWhere.ServicerID, nameof(queryWhere.ServicerID))
                        .AppendLike("B.FCallMemberName", queryWhere.CallMethodName, nameof(queryWhere.CallMethodName))
                        .AppendEqual("B.FSource", queryWhere.Source, nameof(queryWhere.Source))
                        .AppendEqual("B.FRequestGuid", queryWhere.RequestGuid, nameof(queryWhere.RequestGuid))
                        .AppendEqual("A.FNoticeState", queryWhere.NoticeState, nameof(queryWhere.NoticeState))
                        .AppendEqual("A.FDealState", queryWhere.DealState, nameof(queryWhere.DealState))
                ;
            string selectTable = $"{TABLE_NAME_WARNINGLOG} A {SQLSERVER_WITHNOLOCK} {LeftJoin} {TABLE_NAME_RUNTIMELOG} B {SQLSERVER_WITHNOLOCK} {On} A.FRunTimeLogID=B.FID {LeftJoin} {TABLE_NAME_SERVCER} C {SQLSERVER_WITHNOLOCK} {On} C.FID=B.FServicerID  AND C.FIsDeleted=0";

            const string selectColumn = "A.FID,A.FOperateAdvice,a.FTreatmentScheme,A.FNoticeState,A.FDealState,ISNULL(A.FLastModifyTime,A.FCreateTime) FLastModifyTime,B.FLogLevel,B.FCallMemberName,B.FContent,B.FSource,B.FExecuteTime,B.FRequestGuid,B.FProjectName,C.FName AS FServicerName ";
            const string order = "A.FDealState,ISNULL(A.FLastModifyTime,A.FCreateTime)";
            return QueryPageListAsync<WarningLogListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WarningLogDealModel> GetWarningLogDealModel(int id)
        {
            string selectSQL = $"SELECT FID,FOperateAdvice,FTreatmentScheme FROM {TABLE_NAME_WARNINGLOG} WHERE FIsDeleted=0 AND FID=@FID;";
            var sqlQuery = new SqlQuery(selectSQL, new { FID = id });
            return GetDtoAsync<WarningLogDealModel>(sqlQuery);
        }
    }
}