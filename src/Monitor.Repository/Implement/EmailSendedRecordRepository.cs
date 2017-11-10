using JQCore.DataAccess;
using JQCore.DataAccess.Repositories;
using JQCore.DataAccess.Utils;
using JQCore.Result;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository.Implement
{
    /// <summary>
    /// 类名：EmailSendedRecordRepository.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件发送记录数据访问类
    /// 创建标识：template 2017-10-09 17:07:40
    /// </summary>
    public sealed class EmailSendedRecordRepository : BaseDataRepository<EmailSendedRecordInfo>, IEmailSendedRecordRepository
    {
        public EmailSendedRecordRepository(IDataAccessFactory dataAccessFactory) : base(dataAccessFactory, TableConstant.TABLE_NAME_EMAILSENDEDRECORD, DbConnConstant.Conn_Name_Monitor)
        {
        }

        /// <summary>
        /// 获取邮件发送记录列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        public Task<IPageResult<EmailSendRecordListDto>> GetEmailSendRecordListAsync(EmailSendRecordPageQueryWhere queryWhere)
        {
            SqlWhereBuilder whereBuilder = new SqlWhereBuilder("FIsDeleted=0", ReaderDataType);

            const string selectTable = TableConstant.TABLE_NAME_EMAILSENDEDRECORD + SqlConstant.SQLSERVER_WITHNOLOCK;
            const string selectColumn = "FID,FReceiveEmail,FTheme,FContent,FSendEmail,FSendState,FStateRemark,ISNULL(FLastModifyTime,FCreateTime) FLastModifyTime";
            const string order = "ISNULL(FLastModifyTime,FCreateTime) DESC";
            return QueryPageListAsync<EmailSendRecordListDto>(selectColumn, selectTable, whereBuilder.ToString(), order, queryWhere.PageIndex, queryWhere.PageSize, cmdParms: queryWhere);
        }
    }
}