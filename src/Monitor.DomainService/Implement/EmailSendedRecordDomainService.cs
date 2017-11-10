using JQCore.Extensions;
using JQCore.Utils;
using Monitor.Cache;
using Monitor.Constant;
using Monitor.Domain;
using Monitor.Repository;
using System.Threading.Tasks;
using static Monitor.Constant.IgnoreConstant;
using static Monitor.Constant.SysConfigKey;

namespace Monitor.DomainService.Implement
{
    /// <summary>
    /// 类名：EmailSendedRecordDomainService.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件发送记录业务逻辑处理
    /// 创建标识：template 2017-10-09 17:07:40
    /// </summary>
    public sealed class EmailSendedRecordDomainService : IEmailSendedRecordDomainService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmailSendedRecordRepository _emailSendedRecordRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ISysConfigCache _sysConfigCache;

        public EmailSendedRecordDomainService(IProjectRepository projectRepository, IEmailSendedRecordRepository emailSendedRecordRepository, IAdminRepository adminRepository, ISysConfigCache sysConfigCache)
        {
            _projectRepository = projectRepository;
            _emailSendedRecordRepository = emailSendedRecordRepository;
            _adminRepository = adminRepository;
            _sysConfigCache = sysConfigCache;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="theme">主题</param>
        /// <param name="content">内容</param>
        /// <param name="projectID">项目ID</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string theme, string content, int projectID)
        {
            if (projectID <= 0)
            {
                var sendEmailInfo = CreateWaitSendEmail(theme, content, string.Empty, string.Empty);
                sendEmailInfo.NotSend("找不到此项目信息", -1);
                await _emailSendedRecordRepository.InsertOneAsync(sendEmailInfo, "FID", ignoreFields: FID);
            }
            else
            {
                //获取收件人邮箱
                var adminInfo = await _adminRepository.GetInfoAsync(m => m.FState == UserState.Enable && m.FIsDeleted == false);
                var receiveEmailAccount = adminInfo?.FEmail;
                //获取发送邮箱账号密码
                var sendEmailAccount = await _sysConfigCache.GetValueAsync(Email_Account);
                var sendEmailAccountPwd = await _sysConfigCache.GetValueAsync(Email_AccountPwd);
                var sendEmailInfo = CreateWaitSendEmail(theme, content, receiveEmailAccount, sendEmailAccount);
                if (StringUtil.HaveAnyIsNullOrWhiteSpace(receiveEmailAccount, sendEmailAccountPwd, sendEmailAccount))
                {
                    sendEmailInfo.NotSend(GetNotSendRemarkWhenAccountError(receiveEmailAccount, sendEmailAccount, sendEmailAccountPwd), -1);
                }
                else
                {
                    var sendResult = await EmailUtil.SendEmailAsync(receiveEmailAccount, sendEmailInfo.FTheme, sendEmailInfo.FContent, sendEmailAccount, sendEmailAccountPwd, mailboxAddressName: "监控系统");
                    if (sendResult.Item1)
                    {
                        sendEmailInfo.SendSuccess("发送成功", -1);
                    }
                    else
                    {
                        sendEmailInfo.SendFailed(sendResult.Item2, -1);
                    }
                }
                await _emailSendedRecordRepository.InsertOneAsync(sendEmailInfo, "FID", ignoreFields: FID);
            }
        }

        /// <summary>
        /// 创建待发送的邮件
        /// </summary>
        /// <param name="theme">主题</param>
        /// <param name="content">内容</param>
        /// <param name="receiverEmailAccount">接收人账号</param>
        /// <param name="sendEmailAccount">发送人账号</param>
        /// <returns>待发送的邮件</returns>
        public EmailSendedRecordInfo CreateWaitSendEmail(string theme, string content, string receiverEmailAccount, string sendEmailAccount)
        {
            return new EmailSendedRecordInfo
            {
                FContent = content,
                FCreateTime = DateTimeUtil.Now,
                FCreateUserID = -1,
                FIsDeleted = false,
                FSendState = SendState.WaitSend,
                FTheme = theme,
                FReceiveEmail = receiverEmailAccount,
                FSendEmail = sendEmailAccount
            };
        }

        /// <summary>
        /// 获取不发送备注当有账号为空时
        /// </summary>
        /// <param name="receiverEmailAccount">接收人账号</param>
        /// <param name="sendEmailAccount">发送人账号</param>
        /// <param name="sendEmailAccountPwd">发送账号密码</param>
        /// <returns>备注信息</returns>
        private string GetNotSendRemarkWhenAccountError(string receiverEmailAccount, string sendEmailAccount, string sendEmailAccountPwd)
        {
            if (receiverEmailAccount.IsNullOrEmptyWhiteSpace())
            {
                return "接收人邮箱为空";
            }
            if (sendEmailAccount.IsNullOrEmptyWhiteSpace())
            {
                return "发件人邮箱为空";
            }
            if (sendEmailAccountPwd.IsNullOrEmptyWhiteSpace())
            {
                return "发件密码为空";
            }
            return string.Empty;
        }
    }
}