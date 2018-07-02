using GanGao.Common.IModels.IBase;
using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;
using System.Collections.Generic;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 部门定义
    /// </summary>
    public class SysDepartment : EntityBase,
        IDepartment<string>, 
        IParentChildRelation<SysDepartment>
    {
        /// <summary>
        /// 上级部门
        /// </summary>
        public virtual SysDepartment Parent { get; set; }
        /// <summary>
        /// 下级部门集合
        /// </summary>
        public virtual ICollection<SysDepartment> Childs { get; set; }

        #region ///////// 构造器

        public SysDepartment()
        {
            Id = Guid.NewGuid().ToString();
        }

        #endregion
    }

    

}