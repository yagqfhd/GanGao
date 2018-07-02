using System.Collections.Generic;

namespace GanGao.Common.IModels.ISystems
{
    /// <summary>
    /// 实体于部门于角色集合 接口定义
    /// </summary>
    public interface IEntityDepartmentRoleCollection<TEntityDepartmentRole>
        where TEntityDepartmentRole : class
    {
        /// <summary>
        /// 部门下的角色集合
        /// </summary>
        ICollection<TEntityDepartmentRole> Roles { get; }
    }
}
