using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 用户部门角色
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class UserDepartmentRole :Entity , IUserDepartmentRole<string>
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
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 对应的角色
        /// </summary>
        public virtual SysRole Role { get; set; }
    }

    
}