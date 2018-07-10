using GanGao.Common.DToModel.Systems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GanGao.IBLL.Systems
{
    /// <summary>
    /// 服务层 角色信息服务接口
    /// </summary>
    public interface IRoleService : ICoreService<DTORole, string>
    {
        #region ///// 分页相关
        /// <summary>
        /// 获取指定页集合
        /// </summary>
        /// <param name="Skip"></param>
        /// <param name="Limit"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        Task<IEnumerable<DTORole>> PageListAsync(int Skip, int Limit, string Order);
        #endregion

        /// <summary>
        /// 按照名次查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<DTORole> FindByNameAsync(string name);
    }
}