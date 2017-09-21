using JQCore.Web;
using System;
using System.Text;

namespace JQCore.Extensions
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：ExceptionExtension.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：异常扩展类
    /// 创建标识：yjq 2017/9/4 15:29:51
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <returns>错误异常信息</returns>
        public static string ToErrMsg(this Exception ex, string memberName = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            if (ex.InnerException != null)
            {
                if (!string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase))
                {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            if (WebHttpContext.IsHaveHttpContext)
            {
                errorBuilder.AppendFormat("RealIP：{0}", WebHttpContext.RealIP).AppendLine();
                errorBuilder.AppendFormat("HttpRequestUrl：{0}", WebHttpContext.HttpRequestUrl).AppendLine();
                errorBuilder.AppendFormat("UserAgent：{0}", WebHttpContext.UserAgent).AppendLine();
            }
            return errorBuilder.ToString();
        }
    }
}