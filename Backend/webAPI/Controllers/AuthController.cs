using Microsoft.AspNetCore.Mvc;
using webAPI.Models.Request;
using webAPI.Services;

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

            // TODO: 2. Find the user entity in the DB and throw an error if it doesn't exist.

            // TODO: 3. Compare the hashed passwords and throw an error if they do not match.

            // TODO: 4. Create an object containing the JWT token and return it.

            throw new NotImplementedException();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            // TODO: 1. Validate the UserRegisterRequest.

            // TODO: 2. Register the user:
            //              Check for already existing users and throw an error if they are found.
            //              Hash the password.
            //              Create the user object and save it to the database.

            // TODO 3. Create an object containing the JWT token and return it.

            throw new NotImplementedException();
        }
    }
}
