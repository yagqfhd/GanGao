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
            Mapper.Initialize(cfg => cfg.AddProfiles(assembly));
        }        
    }
}