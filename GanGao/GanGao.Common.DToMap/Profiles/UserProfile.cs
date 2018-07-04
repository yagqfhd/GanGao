using AutoMapper;
using GanGao.Common.DToModel.Systems;
using GanGao.Common.Model.Systems;

namespace GanGao.Common.DToMap.Profiles
{
    /// <summary>
    /// 用户转化配置
    /// </summary>
    public class DtoUserProfile : Profile
    {
        public DtoUserProfile()
        {
            CreateMap<DTOUser, SysUser>()
                .ForMember(d => d.PasswordHash, m => m.Ignore()); // 不转化密码
            CreateMap<SysUser, DTOUser>()
                .ForMember(d=>d.PasswordHash , m=>m.Ignore()); // 不转化密码

            CreateMap<UserDepartment, DtoUserDepartment>()
                .ForMember(d => d.Name , mo => mo.MapFrom(dto => dto.Department.Name));

            CreateMap<UserDepartmentRole, DtoUserDepartmentRole>()
                .ForMember(d => d.Name, mo => mo.MapFrom(dto => dto.Role.Name));
        }

        
    }
    //private class MyResolver : IValueResolver<SysUser,DTOUser,>
    //{

    //    public ResolutionResult Resolve(ResolutionResult source)
    //    {
    //        var destinationSubEntity = ((Entity)source.Context.DestinationValue).Sub;

    //        Mapper.Map((Dto)source.Value, destinationSubEntity);

    //        return source.New(destinationSubEntity, typeof(SubEntity));
    //    }
    //}
    //static class MapperConfig2
    //{
    //    private class MyResolver : IValueResolver<>
    //    {

    //        public ResolutionResult Resolve(ResolutionResult source)
    //        {
    //            var destinationSubEntity = ((Entity)source.Context.DestinationValue).Sub;

    //            Mapper.Map((Dto)source.Value, destinationSubEntity);

    //            return source.New(destinationSubEntity, typeof(SubEntity));
    //        }
    //    }

    //    public static void Initialize()
    //    {
    //        Mapper.CreateMap<Dto, Entity>()
    //            .ForMember(entity => entity.Sub, memberOptions => memberOptions.ResolveUsing<MyResolver>());
    //        Mapper.CreateMap<Dto, SubEntity>();
    //    }
    //}
}