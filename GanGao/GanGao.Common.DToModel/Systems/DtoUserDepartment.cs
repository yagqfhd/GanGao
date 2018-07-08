using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{
    /// <summary>
    /// 部门DTO
    /// </summary>
    public class DtoUserDepartment
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        public ICollection<DtoUserDepartmentRole> Roles { get; set; }
    }
}