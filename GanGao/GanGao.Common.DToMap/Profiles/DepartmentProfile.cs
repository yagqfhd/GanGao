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
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DTODepartment, SysDepartment>();
            CreateMap<SysDepartment, DTODepartment>()
               .ForMember(d => d.Parent, mo => mo.MapFrom(dto => dto.Parent.Name));
        }
    }
}