
using System.Collections.Generic;

namespace GanGao.Common.IModels.IBase
{
    /// <summary>
    /// 实体类  基类接口定义
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键定义
        /// </summary>
        TKey Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }
    }

    /// <summary>
    /// 关联类 基类接口定义
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRelation<TKey>
    {        
    }
    /// <summary>
    /// 具有父子关系的类  基类接口定义
    /// </summary>
    public interface IParentChildRelation<TEntity> 
    {
        /// <summary>
        /// 上级，父级
        /// </summary>
        TEntity Parent { get; set; }
        /// <summary>
        /// 下级，子级集合
        /// </summary>
        ICollection<TEntity> Childs { get; set; }
    }    
}
