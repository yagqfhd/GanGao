
using GanGao.Common.Model.Systems;

namespace GanGao.IDAL.ISystems
{
    /// <summary>
    ///     仓储操作接口——用户信息
    /// </summary>
    public interface IUserRepository : IRepository<SysUser>
    {
    }

    /// <summary>
    ///     仓储操作接口——用户部门信息
    /// </summary>
    public interface IUserDepartmentRepository : IRepository<UserDepartment>
    {
    }

    /// <summary>
    ///     仓储操作接口——用户部门信息
    /// </summary>
    public interface IUserDepartmentRoleRepository : IRepository<UserDepartmentRole>
    {
    }
}
