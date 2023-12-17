using webApi.Data.Models;
using webAPI.Interfaces.User;

namespace webAPI.Utils
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this._httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this._userRepository = userRepository;
        }

        public UserModel GetCurrentUser()
        {
            var user = (UserModel) this._httpContextAccessor.HttpContext!.Items["currentUser"]!;
            return user;
        }

        public bool IsAdmin()
        {
            var roles = this._userRepository.GetRolesForUser(this.GetCurrentUser().Id);
            var adminRole = roles.Contains(Role.AdminRole);

            return adminRole;
        }
    }
}
