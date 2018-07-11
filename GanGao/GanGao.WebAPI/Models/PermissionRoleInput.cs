using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    /// <summary>
    /// 权限部门角色输入模型
    /// </summary>
    public class PermissionRoleInput : PermissionDepartmentInput
    {
        public string RoleName { get; set; }
    }    
}