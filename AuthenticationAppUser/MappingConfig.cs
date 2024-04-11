using AuthenticationAppUser.models;
using AuthenticationAppUser.Models;
using AuthenticationAppUser.Models.DTO;
using AutoMapper;

namespace AuthenticationAppUser
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AppUser, RegisterUserDTO>().ReverseMap();
            CreateMap<AddressEntity, RegisterUserDTO>().ReverseMap();
            CreateMap<AddressEntity, RegistrationSchoolDTO>().ReverseMap();
        }
    }
}
