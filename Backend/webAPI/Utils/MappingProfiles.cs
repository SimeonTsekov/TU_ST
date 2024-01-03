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
            CreateMap<UserModel, UserResponse>();
            CreateMap<ActivityRequest, ActivityDataModel>();
            CreateMap<ActivityDataModel, ActivityResponse>();
            CreateMap<HealthDataRequest, HealthDataModel>();
            CreateMap<HealthDataModel, HealthDataResponse>();
            CreateMap<ActivityRecommendationModel, RecommendationResponse>();
            CreateMap<HealthRecommendationModel, RecommendationResponse>();
        }

    }
}
