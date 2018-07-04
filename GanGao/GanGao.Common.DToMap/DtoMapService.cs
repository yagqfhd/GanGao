
using System;
using System.Collections.Generic;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System.ComponentModel.Composition;
using AutoMapper;

namespace GanGao.Common.DToMap
{
    [Export(typeof(IDtoMapService))]
    public class DtoMapService : IDtoMapService
    {
        
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource,TDestination>(source);
        }
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
        
    }
}