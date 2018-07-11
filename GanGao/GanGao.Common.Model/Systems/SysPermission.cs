using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;

namespace GanGao.Common.Model.Systems
{    
    /// <summary>
    /// 权限定义
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class SysPermission : EntityBase<string>,
        IPermission<string>,
        IEntityDepartmentCollection<PermissionDepartment>
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Parameters { get; set; }
        /// <summary>
        /// 权限部门集合
        /// </summary>
        public virtual ICollection<PermissionDepartment> Departments { get; protected set; }

        public SysPermission()
        {
            this.Departments = new HashSet<PermissionDepartment>();
            this.Id = Guid.NewGuid().ToString();
        }
    }
}