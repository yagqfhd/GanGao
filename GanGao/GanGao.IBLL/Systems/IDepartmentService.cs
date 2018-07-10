using GanGao.Common;
using GanGao.Common.DToModel.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GanGao.IBLL.Systems
{
    /// <summary>
    /// 服务层 角色信息服务接口
    /// </summary>
    public interface IDepartmentService : ICoreService<DTODepartment, string>
    {
        #region ///// 分页相关
        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Skip"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        Task<IEnumerable<DTODepartment>> PageListAsync(int Skip, int Limit, string Order);
        #endregion
        /// <summary>
        /// 设置部门的上级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentName"></param>
        /// <returns></returns>
        Task<OperationResult> SetParent(string name, string parentName);
        /// <summary>
        /// 添加部门的下级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        Task<OperationResult> AddChild(string name, string childName);
        /// <summary>
        /// 添加部门的下级部门
        /// </summary>
        /// <param name="name"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        Task<OperationResult> AddChilds(string name, IEnumerable<string> childNames);
    }
}