using AutoMapper;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GanGao.Common.DToMap.Profiles
{
    /// <summary>
    /// 角色转化配置
    /// </summary>
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<DTORole, SysRole>();
            CreateMap<SysRole, DTORole>();
        }
    }
}