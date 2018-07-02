using GanGao.Common.Model.Systems;
using GanGao.Common.Data;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;

namespace GanGao.DAL.Systems
{
    /// <summary>
    ///     仓储操作实现——角色信息
    /// </summary>
    [Export(typeof(IRoleRepository))]
    public class RoleRepository : 
        EFRepositoryBase<SysRole>,
        IRoleRepository
    { }
}