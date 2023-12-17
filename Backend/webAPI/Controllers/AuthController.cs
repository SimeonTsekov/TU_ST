using Microsoft.AspNetCore.Mvc;
using webAPI.DTOs.Request;
using webAPI.Identity;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.Interfaces.Authentication;

namespace webAPI.Controllers
{
    [AllowAnonymousOnly]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Logs the user", Description = "Requires authentication")]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            try
            {
                var result = this._authService.Login(userLoginRequest);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registers the user", Description = "Requires authentication")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var result = this._authService.Register(userRegisterRequest);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }
    }
}
