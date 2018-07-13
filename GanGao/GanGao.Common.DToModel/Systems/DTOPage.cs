using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToModel.Systems
{
    /// <summary>
    /// 分页返回数据格式
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public class DTOPage<TDto>
    {
        public int Total { get; set; } = 0;
        public IEnumerable<TDto> Data { get; set; } 
    }
}