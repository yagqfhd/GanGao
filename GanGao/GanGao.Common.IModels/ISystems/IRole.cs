

using GanGao.Common.IModels.IBase;

namespace GanGao.Common.IModels.ISystems
{
    /// <summary>
    /// 角色类 接口定义
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRole<TKey> : IEntity<TKey>
    {
    }

    /// <summary>
    /// 角色ID 接口定义
    /// </summary>
    public interface IRoleId<TKey>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        TKey RoleId { get; set; }
    }

    /// <summary>
    /// 实体部门角色关系中的角色定义 接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityDepartmentRole<TEntity>
    {
        TEntity Role { get; }
    }
}
