using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    /// <summary>
    /// 权限部门输入模型
    /// </summary>
    public class PermissionDepartmentInput
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "权限名称是必须的")]
        public string PermissionName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "部门名称是必须的")]
        public string DepartmentName { get; set; }
    }
}