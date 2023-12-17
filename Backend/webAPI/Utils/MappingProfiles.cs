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
            CreateMap<UserRequest, UserModel>()
                .AfterMap((src, dest) => dest.SexId = Sex.FromName(src.Sex ?? Sex.Unidentified.Name)!.Value);

            CreateMap<UserModel, UserResponse>()
                .AfterMap((src, dest) => dest.Sex = Sex.FromValue(src.SexId)?.Name);

            CreateMap<ActivityRequest, ActivityDataModel>();
            CreateMap<ActivityDataModel, ActivityResponse>();
            CreateMap<HealthDataRequest, HealthDataModel>();
            CreateMap<HealthDataModel, HealthDataResponse>();
            CreateMap<ActivityRecommendationModel, RecommendationResponse>();
            CreateMap<HealthRecommendationModel, RecommendationResponse>();
        }

    }
}
