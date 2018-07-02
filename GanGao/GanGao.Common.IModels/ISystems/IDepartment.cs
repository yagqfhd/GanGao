using GanGao.Common.IModels.IBase;

namespace GanGao.Common.IModels.ISystems
{
    /// <summary>
    /// 部门类 接口定义
    /// </summary>
    public interface IDepartment<TKey> : IEntity<TKey>
    {
    }
    /// <summary>
    /// 部门角色类 接口定义
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDepartmentRole<TKey> : IRoleId<TKey>, IDepartmentId<TKey>
    {
    }

    /// <summary>
    /// 部门ID 接口定义
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDepartmentId<TKey>
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        TKey DepartmentId { get; set; }
    }
    /// <summary>
    /// 实体部门关系中的部门定义 接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityDepartment<TEntity>
    {
        TEntity Department { get; }
    }
}