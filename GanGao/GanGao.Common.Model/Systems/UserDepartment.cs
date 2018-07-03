using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 用户部门实体对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class UserDepartment  : Entity, 
        IUserDepartment<string>,
        IEntityDepartment<SysDepartment>,
        IEntityDepartmentRoleCollection<UserDepartmentRole>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
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
        public virtual ICollection<UserDepartmentRole> Roles { get; }

        public UserDepartment() { Roles = new HashSet<UserDepartmentRole>(); }
    }

    
}