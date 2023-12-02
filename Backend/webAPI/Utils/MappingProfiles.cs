using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs;

namespace webAPI.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserModel, UserDTO>();
        }
    }
}
