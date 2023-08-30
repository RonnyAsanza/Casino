using AutoMapper;

namespace Casino.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //CreateMap<classmodel1, classmodel2>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
            //CreateMap<classmodel1, classmodel2>();
            //CreateMap<classmodel1, classmodel2>().ReverseMap();
        }
    }
}
