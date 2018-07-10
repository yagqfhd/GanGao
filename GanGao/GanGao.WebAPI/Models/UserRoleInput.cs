using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{

    /// <summary>
    /// 用户部门角色输入模型
    /// </summary>
    public class UserRoleInput : UserDepartmentInput
    {
        public  string RoleName { get; set; }
    }
}