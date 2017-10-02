using JQCore.Dependency;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace JQCore.Web
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：WebHttpContext.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：请求上下文
    /// 创建标识：yjq 2017/9/4 13:21:53
    /// </summary>
    public static class WebHttpContext
    {
        /// <summary>
        /// 获取当前请求上下文
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                var contextAccessor = ContainerManager.Resolve<IHttpContextAccessor>();
                return contextAccessor?.HttpContext;
            }
        }

        /// <summary>
        /// 是否有请求上下文
        /// </summary>
        public static bool IsHaveHttpContext
        {
            get
            {
                return Current != null;
            }
        }

        /// <summary>
        /// 获取绝对Uri
        /// </summary>
        /// <returns>请求的绝对Uri</returns>
        public static string AbsoluteUrl
        {
            get
            {
                if (IsHaveHttpContext)
                    return Current.Request.Host + Current.Request.Path;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <returns>请求地址</returns>
        public static string HttpRequestUrl
        {
            get
            {
                if (IsHaveHttpContext)
                    return Current.Request.Host + Current.Request.Path + Current.Request.QueryString;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取请求类型
        /// </summary>
        /// <returns>请求类型</returns>
        public static string HttpMethod
        {
            get
            {
                if (IsHaveHttpContext)
                    return Current.Request.Method;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取当前网络IP
        /// </summary>
        /// <returns>当前网络IP</returns>
        public static string RealIP
        {
            get
            {
                if (!IsHaveHttpContext) return string.Empty;
                string result = string.Empty;
                if (Current.Request.Headers != null)
                {
                    var forwardedHttpHeader = "X-FORWARDED-FOR";
                    string xff = Current.Request.Headers.Keys
                        .Where(x => forwardedHttpHeader.Equals(x, StringComparison.OrdinalIgnoreCase))
                        .Select(k => Current.Request.Headers[k])
                        .FirstOrDefault();
                    if (!string.IsNullOrEmpty(xff))
                    {
                        string lastIp = xff.Split(new char[] { ',' }).FirstOrDefault();
                        result = lastIp;
                    }
                }
                if (string.IsNullOrEmpty(result) && Current.Connection != null && Current.Connection.RemoteIpAddress != null)
                {
                    result = Current.Connection.RemoteIpAddress.ToString();
                }
                if (result == "::1")
                    result = "127.0.0.1";
                if (!string.IsNullOrEmpty(result))
                {
                    int index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                    if (index > 0)
                        result = result.Substring(0, index);
                }
                else result = "0.0.0.0";
                return result;
            }
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        public static string UserAgent
        {
            get
            {
                if (IsHaveHttpContext)
                {
                    return Current.Request.Headers["User-Agent"];
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断是不是post请求
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static bool IsPost(this HttpRequest httpRequest)
        {
            if (httpRequest == null) return false;
            return string.Equals(httpRequest.Method, "POST", StringComparison.OrdinalIgnoreCase);
        }
    }
}