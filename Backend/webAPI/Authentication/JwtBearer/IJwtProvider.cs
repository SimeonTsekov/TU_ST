using webApi.Data.Models;
using webAPI.DTOs;

namespace webAPI.Authentication.JwtBearer
{
    public interface IJwtProvider
    {
        string Generate(UserModel user);
    }
}
