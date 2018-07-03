using GanGao.Common.IModels.ISystems;
using System.Collections.Generic;
using System.ComponentModel;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 权限部门定义
    /// </summary>
    [Description("权限部门类，用于关联权限部门类")]
    public class PermissionDepartment :Entity, 
        IPermissionDepartment<string>,
        IEntityDepartment<SysDepartment>, // 部门实体
        IEntityDepartmentRoleCollection<PermissionDepartmentRole>
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string PermissionId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门实体
        /// </summary>
        public virtual SysDepartment Department { get; }

        /// <summary>
        /// 部门下的角色集合
        /// </summary>
        public virtual ICollection<PermissionDepartmentRole> Roles { get; }

        public PermissionDepartment()
        {
            Roles = new HashSet<PermissionDepartmentRole>();
        }

    }

    
}