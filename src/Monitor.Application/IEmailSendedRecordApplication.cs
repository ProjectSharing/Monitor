using JQCore.Result;
using Monitor.Trans;
using System.Threading.Tasks;

namespace Monitor.Application
{
    /// <summary>
    /// 类名：IEmailSendedRecordApplication.cs
    /// 接口属性：公共
    /// 类功能描述：邮件发送记录业务逻辑接口
    /// 创建标识：template 2017-10-09 17:07:41
    /// </summary>
    public interface IEmailSendedRecordApplication
    {
        /// <summary>
        /// 获取邮件发送记录
        /// </summary>
        /// <param name="queryWhere">查询条件</param>
        /// <returns></returns>
        Task<OperateResult<IPageResult<EmailSendRecordListDto>>> GetEmailSendRecordListAsync(EmailSendRecordPageQueryWhere queryWhere);
    }
}