using Microsoft.AspNetCore.Builder;
using PrivilegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegeManagement.Infrastructure.CustomMiddleware
{
    /// <summary>
    /// 扩展中间件
    /// </summary>
    public static class AccessPermissionMiddlewareExtensions
    {
        /// <summary>
        /// 引入访问权限控制中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="option">权限中间件配置选项</param>
        /// <returns></returns>
        public static IApplicationBuilder UseAccessPermission(this IApplicationBuilder builder, PermissionMiddlewareOption option)
        {            
            return builder.UseMiddleware<AccessPermissionMiddleware>(option);
        }
    }
}
