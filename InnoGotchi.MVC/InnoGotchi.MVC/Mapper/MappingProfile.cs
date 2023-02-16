using AutoMapper;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfoDto, UserForAuthenticationDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UserInfoDto, UserInfoForUpdateDto>();
        }
    }
}
