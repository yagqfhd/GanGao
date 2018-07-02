using GanGao.Common.Data;
using GanGao.Common.Model.Systems;
using GanGao.IDAL.ISystems;
using System.ComponentModel.Composition;

namespace GanGao.DAL.Systems
{
    /// <summary>
    ///     仓储操作实现——部门信息
    /// </summary>
    [Export(typeof(IDepartmentRepository))]
    public class DepartmentRepository :
        EFRepositoryBase<SysDepartment>, 
        IDepartmentRepository
    { }
}