using Microsoft.AspNetCore.Http;
using PrivilegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PrivilegeManagement.Infrastructure.CustomMiddleware
{
    /// <summary>
    /// 访问许可权限中间件
    /// </summary>
    public class AccessPermissionMiddleware
    {
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 权限中间件的配置选项
        /// </summary>
        private readonly PermissionMiddlewareOption _option;

        /// <summary>
        /// 用户权限集合
        /// </summary>
        internal static List<UserPermission> _userPermissions;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next"></param>
        /// <param name="option"></param>
        public AccessPermissionMiddleware(RequestDelegate next,PermissionMiddlewareOption option)
        {
            _next = next;
            _option = option;
            _userPermissions = option.UserPerssions;
        }

        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            //请求Url
            var questUrl = context.Request.Path.Value.ToLower();

            //是否经过验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (_userPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    //用户名
                    var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;

                    if (_userPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        return this._next(context);
                    }
                    else
                    {                       
                        //无权限跳转到拒绝页面
                        context.Response.Redirect(_option.NoPermissionAction);
                    }
                }
            }
            return this._next(context);
        }
    }

   
}
