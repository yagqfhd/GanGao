using GanGao.Common.IModels.ISystems;

namespace GanGao.Common.Model.Systems
{    
    /// <summary>
    /// 权限部门角色定义
    /// </summary>    
    public class PermissionDepartmentRole : Entity,
        IPermissionDepartmentRole<string>,        
        IEntityDepartmentRole<SysRole>
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
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 对应的角色
        /// </summary>
        public virtual SysRole Role { get; set; }

    }
}