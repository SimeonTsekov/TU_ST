using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface ICurrentUserService
    {
        public UserModel GetCurrentUser();
    }
}
