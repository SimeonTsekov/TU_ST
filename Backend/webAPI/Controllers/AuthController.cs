using Microsoft.AspNetCore.Mvc;
using webAPI.Interfaces;
using webAPI.DTOs.Request;
using webAPI.Identity;
using webAPI.Utils;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymousOnly]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            try
            {
                var result = _authService.Login(userLoginRequest);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymousOnly]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            try
            {
                var result = _authService.Register(userRegisterRequest);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }
    }
}
