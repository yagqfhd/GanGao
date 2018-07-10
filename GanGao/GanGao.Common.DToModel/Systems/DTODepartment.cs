using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{

    /// <summary>
    /// DTO部门
    /// </summary>
    public class DTODepartment
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public ICollection<DTODepartment> Childs { get; set; }
    }
}