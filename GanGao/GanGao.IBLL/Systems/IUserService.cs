﻿using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GanGao.IBLL.Systems
{
    /// <summary>
    /// 服务层 用户信息服务接口
    /// </summary>
    public interface IUserService : ICoreService<DTOUser, string>
    {
        #region 属性

        #endregion

        #region 公共方法 
        #region /// 用户登录相关
        
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<OperationResult> ExistsByNameAsync(string userName);
        
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="emailName"></param>
        /// <returns></returns>
        Task<OperationResult> ExistsByEmailAsync(string emailName);
       
        /// <summary>
        /// 校验用户名称密码
        /// </summary>
        /// <param name="access"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<OperationResult> ValidatorUserAsync(string access, string password);
        #endregion

        #region /// 用户增删改


        #endregion

        #region //// 用户查询
        /// <summary>
        /// 按照ID查询用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DTOUser> FindByIdAsync(string id);
        /// <summary>
        /// 按照名次查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DTOUser> FindByNameAsync(string name);
        /// <summary>
        /// 按照Email查询用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<DTOUser> FindByEmailAsync(string email);
        /// <summary>
        /// 按照用户名获取Email查询用户
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        Task<DTOUser> FindUserAsync(string access);
        /// <summary>
        /// 根据用户名或Email，密码获取用户
        /// </summary>
        /// <param name="access"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<DTOUser> FindUserAsync(string access, string password);
        #endregion

        #region ///// 分页相关
        /// <summary>
        /// 获取指定页用户集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        Task<DTOPage<DTOUser>> UserPageListAsync(int Index, int Limit, string Order);
        #endregion

        #region //// 部门相关
        /// <summary>
        /// 添加用户到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> AddDepartmentAsync(string userName, string departmentName);

        /// <summary>
        /// 移除用户从部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> RemoveDepartmentAsync(string userName, string departmentName);
        #endregion

        #region //// 角色相关
        /// <summary>
        /// 添加用户到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> AddRoleAsync(string userName, string departmentName,string roleName);

        /// <summary>
        /// 移除用户从部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> RemoveRoleAsync(string userName, string departmentName, string roleName);
        #endregion

        #endregion

        #region /////// 权限验证相关
        /// <summary>
        /// 获取用户具有的权限验证特征串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetDepartmentRoleListAsync(string name);
        #endregion
    }
}
