using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GanGao.IBLL.Systems
{
    /// <summary>
    /// 服务层 用户信息服务接口
    /// </summary>
    public interface IPermissionService : ICoreService<DTOPermission, string>
    {
        #region 公共方法 

        #region //// 查询
        
        /// <summary>
        /// 按照名称查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DTOPermission> FindByNameAsync(string name);
        /// <summary>
        /// 按照Controller查询
        /// </summary>
        /// <param name="Controller"></param>
        /// <returns></returns>
        Task<IEnumerable<DTOPermission>> FindByControllerAsync(string controller);
        /// <summary>
        /// 按照action查询
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        Task<IEnumerable<DTOPermission>> FindByActionAsync(string action);
        
        #endregion

        #region ///// 分页相关
        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        Task<IEnumerable<DTOPermission>> PageListAsync(int Index, int Limit, string Order);
        #endregion

        #region //// 部门相关
        /// <summary>
        /// 添加到部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> AddDepartmentAsync(string name, string departmentName);

        /// <summary>
        /// 移除从部门中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> RemoveDepartmentAsync(string name, string departmentName);
        #endregion

        #region //// 角色相关
        /// <summary>
        /// 添加到部门角色中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> AddRoleAsync(string name, string departmentName,string roleName);

        /// <summary>
        /// 移除从部门角色中
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        Task<OperationResult> RemoveRoleAsync(string name, string departmentName, string roleName);
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
