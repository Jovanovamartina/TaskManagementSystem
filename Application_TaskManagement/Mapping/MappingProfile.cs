

using Application_TaskManagement.DTOs;
using AutoMapper;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsCreateDto, News>().ReverseMap();
            //CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<News, NewsDto>()
     .ReverseMap()
     .ForMember(dest => dest.Id, opt => opt.Ignore())
     .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
     .ForMember(dest => dest.CreatedByUserId, opt => opt.Ignore())
     .ForAllMembers(opts =>
         opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
