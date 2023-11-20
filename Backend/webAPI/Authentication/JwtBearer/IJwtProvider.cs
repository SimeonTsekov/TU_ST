using webAPI.Models;

namespace webAPI.Authentication.JwtBearer
{
    public interface IJwtProvider
    {
        string Generate(UserModel user);
    }
}
