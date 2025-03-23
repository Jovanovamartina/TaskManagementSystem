

using Application_TaskManagement.DTOs;
using AutoMapper;
using Core_TaskManagement.Entities;

namespace Application_TaskManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserModel, ApplicationUser>().ReverseMap();

        }
    }
}
