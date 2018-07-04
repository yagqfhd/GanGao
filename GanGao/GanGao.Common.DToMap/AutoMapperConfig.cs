using AutoMapper;
using GanGao.Common.DToMap.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToMap
{
    public class AutoMapperProfileRegister
    {
        public static void Register()
        {
            var assembly = typeof(IAutoMapperProfile).Assembly;
#if DEBUG
            var types = assembly.GetTypes();
            Console.WriteLine("typeof(IAutoMapperProfile).GetTypes().Count()={0}", types.Count());
            foreach (var type in types)
            {
                Console.WriteLine("Type:{0}", type.Name);
            }
            
#endif 
            Mapper.Initialize(cfg => cfg.AddProfiles(assembly));
        }        
    }
}