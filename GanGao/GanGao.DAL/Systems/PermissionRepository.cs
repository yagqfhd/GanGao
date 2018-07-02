using GanGao.Common.Model.Systems;
using GanGao.Common.Data;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;

namespace GanGao.DAL.Systems
{
    /// <summary>
    ///     仓储操作实现——角色信息
    /// </summary>
    [Export(typeof(IPermissionRepository))]
    public class PermissionRepository : 
        EFRepositoryBase<SysPermission>, 
        IPermissionRepository
    { }
    /// <summary>
    ///     仓储操作实现——角色信息
    /// </summary>
    [Export(typeof(IPermissionDepartmentRepository))]
    public class PermissionDepartmentRepository : 
        EFRepositoryBase<PermissionDepartment>, 
        IPermissionDepartmentRepository
    { }
    /// <summary>
    ///     仓储操作实现——角色信息
    /// </summary>
    [Export(typeof(IPermissionDepartmentRoleRepository))]
    public class PermissionDepartmentRoleRepository : 
        EFRepositoryBase<PermissionDepartmentRole>, 
        IPermissionDepartmentRoleRepository
    { }

}