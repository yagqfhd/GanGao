using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{

    /// <summary>
    /// DTO角色
    /// </summary>
    public class DTORole
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="角色名称是必须的")]
        [StringLength(maximumLength:16,ErrorMessage ="角色名称最小1个字符最大16个字符",MinimumLength =1)]
        public string Name { get; set; }
        [StringLength(maximumLength: 128, ErrorMessage = "角色说明最大128个字符")]
        public string Description { get; set; }
    }
}