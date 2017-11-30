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
    /// 类名：WarningSqlRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SQL预警信息数据访问类
    /// 创建标识：template 2017-11-29 22:17:28
    /// </summary>
    public sealed class WarningSqlRepository : BaseDataRepository<WarningSqlInfo>, IWarningSqlRepository
    {
        public WarningSqlRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_WARNINGSQL, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 获取预警日志列表
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        public Task<IPageResult<WarningSqlListDto>> GetWarningSqlListAsync(WarningSqlPageQueryWhere queryWhere)
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
            string selectTable = $"{TABLE_NAME_WARNINGSQL} A {SQLSERVER_WITHNOLOCK} {LeftJoin} {TABLE_NAME_RUNTIMESQL} B {SQLSERVER_WITHNOLOCK} {On} A.FRuntimeSqlID=B.FID {LeftJoin} {TABLE_NAME_SERVCER} C {SQLSERVER_WITHNOLOCK} {On} C.FID=B.FServicerID  AND C.FIsDeleted=0";
            const string selectColumn = "A.FID,A.FOperateAdvice,a.FTreatmentScheme,A.FNoticeState,A.FDealState,ISNULL(A.FLastModifyTime,A.FCreateTime) FLastModifyTime,B.FMemberName AS FCallMemberName,B.FSqlText AS FContent,B.FSource,B.FExecutedTime,B.FRequestGuid,B.FProjectName,C.FName AS FServicerName ";
            const string order = "A.FDealState,ISNULL(A.FLastModifyTime,A.FCreateTime)";
            return QueryPageListAsync<WarningSqlListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }

        /// <summary>
        /// 获取处理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<WarningDealModel> GetWarningDealModelAsync(int id)
        {
            string selectSQL = $"SELECT FID,FOperateAdvice,FTreatmentScheme FROM {TABLE_NAME_WARNINGSQL} WHERE FIsDeleted=0 AND FID=@FID;";
            var sqlQuery = new SqlQuery(selectSQL, new { FID = id });
            return GetDtoAsync<WarningDealModel>(sqlQuery);
        }
    }
}