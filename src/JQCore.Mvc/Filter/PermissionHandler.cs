using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JQCore.Mvc.Filter
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：PermissionHandler.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：
    /// 创建标识：yjq 2017/9/26 17:18:45
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<LoginRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoginRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                context.Succeed(requirement);
            }
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            return Task.CompletedTask;
        }
    }
}