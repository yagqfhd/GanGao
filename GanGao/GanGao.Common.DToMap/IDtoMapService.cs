
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System.Collections.Generic;

namespace GanGao.Common.DToMap
{
    /// <summary>
    /// DTO模型映射到Model的服务接口定义
    /// </summary>
    public interface IDtoMapService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
