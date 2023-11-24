using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces
{
    public interface IUserService
    {
        JwtResponse Login(UserLoginRequest loginRequest);

        JwtResponse Register(UserRegisterRequest registerRequest);
    }
}
