using System.Collections.Generic;

namespace GanGao.Common.IModels.ISystems
{
    /// <summary>
    /// 实体的部门集合 接口定义
    /// </summary>
    public interface IEntityDepartmentCollection<TEntityDepartment>
        where TEntityDepartment : class
    {
        /// <summary>
        /// 实体的部门集合
        /// </summary>
        ICollection<TEntityDepartment> Departments { get; }
    }
}
