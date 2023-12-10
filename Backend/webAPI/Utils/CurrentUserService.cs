using webApi.Data.Models;
using webAPI.Interfaces;

namespace webAPI.Utils
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public UserModel GetCurrentUser()
        {
            var user = (UserModel) this._httpContextAccessor.HttpContext!.Items["currentUser"]!;
            return user;
        }
    }
}
