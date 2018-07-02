using GanGao.IDAL;
using GanGao.MEF;
using System.ComponentModel.Composition;

namespace GanGao.BLL
{
    /// <summary>
    /// 核心业务实现基类
    /// </summary>
    public abstract class CoreServiceBase
    {
        public CoreServiceBase()
        {
            RegisgterMEF.regisgter().ComposeParts(this);
        }
        /// <summary>
        /// 获取或设置 工作单元对象，用于处理同步业务的事务操作
        /// </summary>
        [Import]
        protected IUnitOfWork UnitOfWork { get; set; }        
    }
}