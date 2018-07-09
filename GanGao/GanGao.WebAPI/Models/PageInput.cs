using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.WebAPI.Models
{
    public class PageInput
    {
        public int page { get; set; } = 1;
        public int limit { get; set; } = 10;
    }
}