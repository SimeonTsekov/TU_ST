using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs;
using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRequest, UserModel>();
            
            CreateMap<UserModel, UserResponse>()
                .ForMember(dest => dest.ActivityData, opt => opt.MapFrom(src => src.ActivityDataModels))
                .ForMember(dest => dest.HealthData, opt => opt.MapFrom(src => src.HealthDataModels));
            
            CreateMap<ActivityRequest, ActivityDataModel>();
            CreateMap<ActivityDataModel, ActivityResponse>();
            CreateMap<HealthDataRequest, HealthDataModel>();
            CreateMap<HealthDataModel, HealthDataResponse>();
        }
    }
}
