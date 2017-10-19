using JQCore.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Monitor.Web
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：BaseController.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/26 17:06:01
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "Permission")]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前管理员ID
        /// </summary>
        public int AdminID
        {
            get
            {
                if (HttpContext.User != null)
                {
                    var adminClaim = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Sid);
                    if (adminClaim != null)
                    {
                        return adminClaim.Value.ToSafeInt32(0);
                    }
                }
                return 0;
            }
        }
    }
}