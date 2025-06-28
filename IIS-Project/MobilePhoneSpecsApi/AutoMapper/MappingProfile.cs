using AutoMapper;
using MobilePhoneSpecsApi.DTOs;
using MobilePhoneSpecsApi.Models;

namespace MobilePhoneSpecsApi.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Specification, SpecificationDto>().ReverseMap();
            CreateMap<PhoneDetails, PhoneDetailsDto>().ReverseMap();
            CreateMap<GsmLaunchDetails, GsmLaunchDetailsDto>().ReverseMap();
            CreateMap<GsmBodyDetails, GsmBodyDetailsDto>().ReverseMap();
            CreateMap<GsmDisplayDetails, GsmDisplayDetailsDto>().ReverseMap();
            CreateMap<GsmSoundDetails, GsmSoundDetailsDto>().ReverseMap();
            CreateMap<GsmBatteryDetails, GsmBatteryDetailsDto>().ReverseMap();
            CreateMap<GsmMemoryDetails, GsmMemoryDetailsDto>().ReverseMap();
            CreateMap<User, UserDto>();
        }
    }
}
