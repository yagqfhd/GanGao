
using GanGao.Common.Data;
using GanGao.Common.Model.Systems;

namespace GanGao.IDAL.ISystems
{
    /// <summary>
    ///     仓储操作接口——权限信息
    /// </summary>
    public interface IPermissionRepository : IRepository<SysPermission>
    { }

    /// <summary>
    ///     仓储操作接口——权限部门信息
    /// </summary>
    public interface IPermissionDepartmentRepository : IRepository<PermissionDepartment>
    { }

    /// <summary>
    ///     仓储操作接口——权限部门信息
    /// </summary>
    public interface IPermissionDepartmentRoleRepository : IRepository<PermissionDepartmentRole>
    { }
}
