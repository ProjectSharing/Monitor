using Microsoft.AspNetCore.Http;
using System;

namespace JQCore.Web
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：CookieUtil.cs
    /// 类属性：公共类（静态）
    /// 类功能描述：cookie工具类
    /// 创建标识：yjq 2017/9/4 14:20:19
    /// </summary>
    public static class CookieUtil
    {
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookie(string cookieKey)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
            {
                return string.Empty;
            }
            if (!WebHttpContext.IsHaveHttpContext)
            {
                return string.Empty;
            }
            return WebHttpContext.Current.Request.Cookies[cookieKey];
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        public static void SetCookie(string cookieKey, string cookieValue)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
                return;
            SetCookie(cookieKey, cookieValue, expires: null);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookieKey, string cookieValue, DateTime? expires)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
                return;
            CookieOptions cookieOption = new CookieOptions
            {
                Expires = expires
            };
            WebHttpContext.Current.Response.Cookies.Append(cookieKey, cookieValue, cookieOption);
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieKey">cookie名</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="cookieOption">cookie配置信息</param>
        public static void SetCookie(string cookieKey, string cookieValue, CookieOptions cookieOption)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
                return;
            WebHttpContext.Current.Response.Cookies.Append(cookieKey, cookieValue, cookieOption);
        }

        /// <summary>
        /// 移除一个cookie
        /// </summary>
        /// <param name="cookieKey">cookie名</param>
        public static void RemoveCookie(string cookieKey)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
                return;
            WebHttpContext.Current.Response.Cookies.Delete(cookieKey);
        }

        /// <summary>
        /// 移除一个cookie
        /// </summary>
        /// <param name="cookieKey">cookie名</param>
        /// <param name="cookieOption">cookie配置信息</param>
        public static void RemoveCookie(string cookieKey, CookieOptions cookieOption)
        {
            if (string.IsNullOrWhiteSpace(cookieKey))
                return;
            WebHttpContext.Current.Response.Cookies.Delete(cookieKey, cookieOption);
        }
    }
}