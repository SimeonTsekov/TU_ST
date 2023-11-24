using Microsoft.AspNetCore.Mvc;
using webAPI.Interfaces;
using webAPI.DTOs.Request;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            // TODO: 1. Validate the UserLoginRequest.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: 2. Attempt to log in the user.
            var result = _userService.Login(userLoginRequest);

            if (result == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // TODO: 3. Return the JWT token.
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            // TODO: 1. Validate the UserRegisterRequest.v
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // TODO: 2. Register the user:
            //              Check for already existing users and throw an error if they are found.
            //              Hash the password.
            //              Create the user object and save it to the database.

            // TODO 3. Create an object containing the JWT token and return it.

            throw new NotImplementedException();
        }
    }
}
