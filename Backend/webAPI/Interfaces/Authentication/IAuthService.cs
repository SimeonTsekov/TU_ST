using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.Authentication
{
    public interface IAuthService
    {
        JwtResponse Login(UserLoginRequest loginRequest);
        JwtResponse Register(UserRegisterRequest registerRequest);
    }
}
