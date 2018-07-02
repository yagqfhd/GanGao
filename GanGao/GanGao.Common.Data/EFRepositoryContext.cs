using GanGao.IDAL;
using System.ComponentModel.Composition;
using System.Data.Entity;


namespace GanGao.Common.Data
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof (IUnitOfWork))]
    internal class EFRepositoryContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get { return EFDbContext; }
        }

        [Import(typeof(DbContext))]
        private EFDbContext EFDbContext { get; set; }
    }
}