using AutoMapper;
using GanGao.Common.DToMap.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToMap
{
    public class AutoMapperConfig
    {
        [ImportMany]
        public IEnumerable<IAutoMapperProfile> Profiles { get; set; }

        public void Register()
        {
            Mapper.Configuration.GetAllTypeMaps();
            //Mapper.Configuration.AddProfile(new ViewModelToModelProfile());
        }

        public void RegistProfiles()
        {
            if (Profiles == null)
            {
                //return;
                throw PublicHelper.ThrowDataAccessException("AutoMapper 配置映射对象个数为0。");
            }
            
            Mapper.Initialize(cfg => {
                foreach (var mapper in Profiles)
                {                    
                    cfg.AddProfile(mapper as Profile);
                }
                
            });
            
        }
    }
}