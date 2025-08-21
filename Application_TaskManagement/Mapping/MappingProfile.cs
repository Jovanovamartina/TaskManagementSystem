

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
            CreateMap<News, NewsDto>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
