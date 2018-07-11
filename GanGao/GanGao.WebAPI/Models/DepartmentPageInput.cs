using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    public class DepartmentPageInput : PageInput
    {
        public string Parent { get; set; } = null;
    }
}