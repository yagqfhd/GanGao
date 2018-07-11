using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{
    /// <summary>
    /// DTO权限
    /// </summary>
    public class DTOPermission
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称是必须的")]
        public string Name { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Parameters { get; set; }
        [StringLength(maximumLength: 128, ErrorMessage = "说明最大128个字符")]
        public string Description { get; set; }
        public ICollection<DtoPermissionDepartment> Departments { get; set; }
    }
}