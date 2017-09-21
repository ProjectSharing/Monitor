using JQCore.Configuration;
using JQCore.Utils;
using System.Threading.Tasks;

namespace Monitor.Infrastructure
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：SmtpMailUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：SmtpMailUtil
    /// 创建标识：yjq 2017/9/21 22:04:01
    /// </summary>
    public static class SmtpMailUtil
    {
        private static readonly string _ConfigKey_ServiceMailAddress = "EmailAccount";

        private static readonly string _ConfigKey_ServiceMailPwd = "EmailPwd";

        /// <summary>
        /// 发送邮件(smtp协议)
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        public static bool SendEmail(string to, string subject, string content)
        {
            return EmailUtil.SendEmail(new string[] { to }, subject, content, ConfigurationManage.GetValue(_ConfigKey_ServiceMailAddress), ConfigurationManage.GetValue(_ConfigKey_ServiceMailPwd));
        }

        /// <summary>
        /// 异步发送邮件(smtp协议)
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        public static Task<bool> SendEmailAsync(string to, string subject, string content)
        {
            return EmailUtil.SendEmailAsync(new string[] { to }, subject, content, ConfigurationManage.GetValue(_ConfigKey_ServiceMailAddress), ConfigurationManage.GetValue(_ConfigKey_ServiceMailPwd));
        }
    }
}