using System.Security.Claims;
using webAPI.Interfaces.User;

namespace webAPI.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var claimValue = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (claimValue != null)
            {
                var userId = int.Parse(claimValue);

                var serviceProvider = context.RequestServices;
                var userRepository = serviceProvider.GetRequiredService<IUserRepository>();

                var userModel = userRepository.GetUserById(userId);
                    
                context.Items["currentUser"] = userModel;
            }

            await _next(context);
        }
    }
}
