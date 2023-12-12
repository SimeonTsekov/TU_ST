using webApi.Data.Models;

namespace webAPI.Interfaces.Authentication
{
    public interface IJwtProvider
    {
        string Generate(UserModel user);
    }
}
