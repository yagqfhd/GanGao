using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{
    /// <summary>
    /// 部门DTO
    /// </summary>
    public class DtoPermissionDepartment
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        public ICollection<DtoPermissionDepartmentRole> Roles { get; set; }
    }
}