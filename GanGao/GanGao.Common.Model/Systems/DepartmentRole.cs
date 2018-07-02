using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 部门角色类定义
    /// </summary>
    [Description("部门角色类，用于验证权限")]
    public class DepartmentRole : IDepartmentRole<string>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentId { get; set; }
    }
    
    
}