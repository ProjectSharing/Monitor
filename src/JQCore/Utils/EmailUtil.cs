using JQCore.Extensions;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Text;
using System.Threading.Tasks;

namespace JQCore.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：EmailUtil.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件工具
    /// 创建标识：yjq 2017/9/15 17:09:12
    /// </summary>
    public static class EmailUtil
    {
        /// <summary>
        /// 发送邮件(smtp协议)
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="serviceMailAddress">服务器邮箱账号</param>
        /// <param name="serviceMailPwd">服务器邮箱密码</param>
        /// <param name="mailboxAddressName">服务端邮箱名字</param>
        /// <returns></returns>
        public static ValueTuple<bool, string> SendEmail(string to, string subject, string content, string serviceMailAddress, string serviceMailPwd, string mailboxAddressName = null)
        {
            return SendEmail(new string[] { to }, subject, content, serviceMailAddress, serviceMailPwd, mailboxAddressName: mailboxAddressName);
        }

        /// <summary>
        /// 异步发送邮件(smtp协议)
        /// </summary>
        /// <param name="to">接收人</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="serviceMailAddress">服务器邮箱账号</param>
        /// <param name="serviceMailPwd">服务器邮箱密码</param>
        /// <param name="mailboxAddressName">服务端邮箱名字</param>
        /// <returns></returns>
        public static async Task<ValueTuple<bool, string>> SendEmailAsync(string to, string subject, string content, string serviceMailAddress, string serviceMailPwd, string mailboxAddressName = null)
        {
            return await SendEmailAsync(new string[] { to }, subject, content, serviceMailAddress, serviceMailPwd, mailboxAddressName: mailboxAddressName);
        }

        /// <summary>
        /// 异步发送邮件(smtp协议)
        /// </summary>
        /// <param name="toList">发送列表</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="serviceMailAddress">服务端地址</param>
        /// <param name="serviceMailPwd">服务端密码</param>
        /// <param name="mailboxAddressName">服务端邮箱名字</param>
        /// <returns></returns>
        public static Task<ValueTuple<bool, string>> SendEmailAsync(string[] toList, string subject, string content, string serviceMailAddress, string serviceMailPwd, string mailboxAddressName = null)
        {
            return Task.FromResult(SendEmail(toList, subject, content, serviceMailAddress, serviceMailPwd, mailboxAddressName: mailboxAddressName));
        }

        /// <summary>
        /// 发送邮件(smtp协议)
        /// </summary>
        /// <param name="toList">发送列表</param>
        /// <param name="subject">发送主题</param>
        /// <param name="content">发送内容</param>
        /// <param name="serviceMailAddress">服务端地址</param>
        /// <param name="serviceMailPwd">服务端密码</param>
        /// <param name="mailboxAddressName">服务端邮箱名字</param>
        /// <returns></returns>
        public static ValueTuple<bool, string> SendEmail(string[] toList, string subject, string content, string serviceMailAddress, string serviceMailPwd, string mailboxAddressName = null)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(mailboxAddressName ?? "系统邮件", serviceMailAddress));
                toList.ForEach(to =>
                {
                    message.To.Add(new MailboxAddress(to));
                });
                message.Subject = subject;
                var textPart = new TextPart(TextFormat.Plain);
                textPart.SetText(Encoding.UTF8, content);
                message.Body = textPart;
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.163.com", 25, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(serviceMailAddress, serviceMailPwd);

                    client.Send(message);
                    client.Disconnect(true);
                }
                return ValueTuple.Create(true, string.Empty);
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex, memberName: "SendEmail");
                return ValueTuple.Create(false, ex.Message);
            }
        }
    }
}