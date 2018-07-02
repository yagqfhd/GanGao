using GanGao.Common.IModels.ISystems;
using GanGao.Common.Model.Bases;
using System;

namespace GanGao.Common.Model.Systems
{
    /// <summary>
    /// 角色定义 
    /// </summary>
    public class SysRole : EntityBase, IRole<string>
    {
        public SysRole() { Id = Guid.NewGuid().ToString(); }
    }


}