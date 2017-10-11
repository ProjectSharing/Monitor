using System.Threading.Tasks;

namespace Monitor.DomainService
{
    /// <summary>
    /// 类名：IEmailSendedRecordDomainService.cs
    /// 接口属性：公共
    /// 类功能描述：邮件发送记录领域服务接口
    /// 创建标识：template 2017-10-09 17:07:40
    /// </summary>
    public interface IEmailSendedRecordDomainService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="theme">主题</param>
        /// <param name="content">内容</param>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        Task SendEmailAsync(string theme, string content, int projectID);
    }
}