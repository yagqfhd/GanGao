using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        //[ForeignKey("Department")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门实体
        /// </summary>
        //[ForeignKey("DepartmentId")]
        public virtual SysDepartment Department { get; set; } //virtual 

        /// <summary>
        /// 部门下的角色集合
        /// </summary>
        public virtual ICollection<UserDepartmentRole> Roles { get; }

        public UserDepartment() { Roles = new HashSet<UserDepartmentRole>(); }
    }

    
}