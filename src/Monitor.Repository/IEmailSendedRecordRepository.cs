using JQCore.DataAccess.Repositories;
using JQCore.Result;
using Monitor.Domain;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Repository
{
    /// <summary>
    /// 类名：IEmailSendedRecordRepository.cs
    /// 接口属性：公共
    /// 类功能描述：邮件发送记录数据访问接口
    /// 创建标识：template 2017-10-09 17:07:40
    /// </summary>
    public interface IEmailSendedRecordRepository : IBaseDataRepository<EmailSendedRecordInfo>
    {
        /// <summary>
        /// 获取邮件发送记录列表
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        Task<IPageResult<EmailSendRecordListDto>> GetEmailSendRecordListAsync(EmailSendRecordPageQueryWhere queryWhere);
    }
}