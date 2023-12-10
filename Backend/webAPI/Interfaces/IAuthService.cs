using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces
{
    public interface IAuthService
    {
        JwtResponse Login(UserLoginRequest loginRequest);
        JwtResponse Register(UserRegisterRequest registerRequest);
    }
}
