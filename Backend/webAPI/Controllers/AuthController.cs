using Microsoft.AspNetCore.Mvc;
using webAPI.Interfaces;
using webAPI.DTOs.Request;

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
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authService.Login(userLoginRequest);

            if (result == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            // TODO: 1. Validate the UserRegisterRequest.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: 2. Attempt to register the user.
            var result = _authService.Register(userRegisterRequest);

            if (result == null)
            {
                return Conflict("Username already taken");
            }

            // TODO: 3. Return the JWT token.
            return Ok(result);
        }
    }
}
