using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using PolicyPrivilegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyPrivilegeManagement.Infrastructure.CustomAuthorizeHandler
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /*
         类必需继承AuthorizationHandler<T>，只用实现public virtual Task HandleAsync(AuthorizationHandlerContext context)，些方法是用户请求时验证是否授权的主方法
         */

        /// <summary>
        /// 用户权限集
        /// </summary>
        public List<UserPermission> UserPermissions { get; set; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //赋值用户权限
            UserPermissions = requirement.UserPermissions;

            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;

            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();

            //确认用户是否认证
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if (UserPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    //用户名
                    var userName = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;

                    if (UserPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        httpContext.Response.Redirect("/Denied");
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }

            }

            return Task.CompletedTask;

        }

    }
}
