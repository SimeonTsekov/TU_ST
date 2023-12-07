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
        private readonly IHealthDataService _healthService;
        private readonly IUserRepository _userRepository;

        public UserController(IActivityService activityService, IUserRepository userRepository, IHealthDataService healthService)
        {
            _userRepository = userRepository;
            _activityService = activityService;
            _healthService = healthService;
        }

        [HttpPut("{userId}")]
        [SwaggerOperation(Summary = "Updates an existing user", Description = "Requires authentication")]
        public IActionResult Update(int userId, [FromBody] UserModel updatedUser)
        {
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user data");
            }
        }

        [HttpDelete("{userId}")]
        [SwaggerOperation(Summary = "Deletes the user", Description = "Requires authentication")]
        public IActionResult Delete(int userId)
        {
            _userRepository.Delete(userId);

            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all users", Description = "Requires authentication")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "Gets a certain user", Description = "Requires authentication")]
        public IActionResult GetUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            return Ok(user);
        }

        [HttpGet("{userId}/activities")]
        [SwaggerOperation(Summary = "Gets activities of a certain user", Description = "Requires authentication")]
        public IActionResult GetActivitiesForUser(int userId)
        {
            var activityDataById = this._activityService.GetAllByUserId(userId);

            return Ok(activityDataById);
        }

        [HttpGet("{userId}/healthData")]
        [SwaggerOperation(Summary = "Gets health data of a certain user", Description = "Requires authentication")]
        public IActionResult GetHealthDataForUser(int userId)
        {
            var healthDataById = _healthService.GetAllByUserId(userId);

            return Ok(healthDataById);
        }
    }
}
 