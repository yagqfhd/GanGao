using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    /// <summary>
    /// 用户部门输入模型
    /// </summary>
    public class UserDepartmentInput
    {
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
    }
}