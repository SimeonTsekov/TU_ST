using webAPI.Models.Request;
using webAPI.Models.Response;

namespace webAPI.Services
{
    public interface IUserService
    {
        JwtResponse Login(UserLoginRequest loginRequest);

        JwtResponse Register(UserRegisterRequest registerRequest);
    }
}
