using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    /// <summary>
    /// 部门上级下级关系输入模型
    /// </summary>
    public class DepartmentParentInput
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "上级是必须的")]
        public string Parent { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "下级是必须的")]
        public string Child { get; set; }
    }
}