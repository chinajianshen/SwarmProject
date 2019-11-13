using Microsoft.AspNetCore.Authorization;
using PolicyPrivilegeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyPrivilegeManagement.Infrastructure.CustomAuthorizeHandler
{
    /// <summary>
    /// 必要参数类
    /// </summary>
    public class PermissionRequirement :IAuthorizationRequirement
    {
        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<UserPermission> UserPermissions { get; private set; }

        /// <summary>
        /// 无权限action
        /// </summary>
        public string DeniedAction { get; set; }

        public PermissionRequirement(string deniedAction,List<UserPermission> userPermissions)
        {
            this.DeniedAction = deniedAction;
            this.UserPermissions = userPermissions;
        }
    }
}
