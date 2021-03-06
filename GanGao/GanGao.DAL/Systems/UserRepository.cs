﻿using GanGao.Common.Model.Systems;
using GanGao.Common.Data;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;

namespace GanGao.DAL.Systems
{
    /// <summary>
    ///     仓储操作实现——用户信息
    /// </summary>
    [Export(typeof(IUserRepository))]
    public class UserRepository : 
        EFRepositoryBase<SysUser>, 
        IUserRepository
    { }
    /// <summary>
    ///     仓储操作实现——用户部门信息
    /// </summary>
    [Export(typeof(IUserDepartmentRepository))]
    public class UserDepartmentRepository : 
        EFRepositoryBase<UserDepartment>, 
        IUserDepartmentRepository
    { }
    /// <summary>
    ///     仓储操作实现——用户部门角色信息
    /// </summary>
    [Export(typeof(IUserDepartmentRoleRepository))]
    public class UserDepartmentRoleRepository : 
        EFRepositoryBase<UserDepartmentRole>, 
        IUserDepartmentRoleRepository
    { }

}