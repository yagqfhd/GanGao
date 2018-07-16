using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{

    /// <summary>
    /// DTO部门
    /// </summary>
    public class DTODepartment
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称是必须的")]
        public string Name { get; set; }
        [StringLength(maximumLength: 128, ErrorMessage = "角色说明最大128个字符")]
        public string Description { get; set; }
        public string Parent { get; set; }
        public ICollection<DTODepartment> Childs { get; set; }
    }
}