using webApi.Data.Models;

namespace webAPI.Interfaces.User
{
    public interface ICurrentUserService
    {
        public UserModel GetCurrentUser();
    }
}
