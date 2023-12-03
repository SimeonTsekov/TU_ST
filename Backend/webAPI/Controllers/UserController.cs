using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Data.Models;
using webAPI.Interfaces;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IUserRepository _userRepository;

        public UserController(IActivityService activityService, IUserRepository userRepository)
        {
            _activityService = activityService;
            _userRepository = userRepository;
        }

        // [HttpPost]
        // public IActionResult Create()
        // {
        //     throw new NotImplementedException();
        //     //_userService.create()
        // }

        [HttpPut("{userId}")]
        [SwaggerOperation(Summary = "Updates an existing user", Description = "Requires authentication")]
        public IActionResult Update(int userId, [FromBody] UserModel updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userToUpdate = _userRepository.GetUserById(userId);
                if (userToUpdate == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                var updatedUserInfo = _userRepository.Update(userId, updatedUser);
                return Ok(updatedUserInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user data");
            }
        }

        [HttpDelete("{userId}")]
        [SwaggerOperation(Summary = "Deletes the user", Description = "Requires authentication")]
        public IActionResult Delete(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _userRepository.Delete(userId);

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all users", Description = "Requires authentication")]
        public IActionResult GetAllUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "Gets a certain user", Description = "Requires authentication")]
        public IActionResult GetUser(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.GetUserById(userId);

            return Ok(user);
        }

        [HttpGet("{userId}/activities")]
        [SwaggerOperation(Summary = "Gets activities of a certain user", Description = "Requires authentication")]
        public IActionResult GetActivitiesForUser(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allByUserId = this._activityService.GetAllByUserId(userId);

            return Ok(allByUserId);
        }
    }
}
 