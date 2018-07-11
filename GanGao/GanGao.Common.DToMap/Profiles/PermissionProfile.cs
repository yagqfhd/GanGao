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
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<DTOPermission, SysPermission>()
                .ForMember(d => d.Departments, m => m.Ignore());
            CreateMap<SysPermission, DTOPermission>();
            //CreateMap<SysDepartment,DTODepartment>()
            CreateMap<PermissionDepartment, DtoPermissionDepartment>()
               .ForMember(d => d.Name, mo => mo.MapFrom(dto => dto.Department.Name));

            CreateMap<PermissionDepartmentRole, DtoPermissionDepartmentRole>()
                .ForMember(d => d.Name, mo => mo.MapFrom(dto => dto.Role.Name));
        }
    }
}