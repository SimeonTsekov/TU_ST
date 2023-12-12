using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Data.Models;
using webAPI.DTOs.Request;
using webAPI.Interfaces;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IHealthDataService _healthService;
        private readonly IUserService _userService;

        public UserController(IActivityService activityService, IHealthDataService healthService, IUserService userService)
        {
            _activityService = activityService;
            _healthService = healthService;
            _userService = userService;
        }

        [HttpPut("{userId}")]
        [SwaggerOperation(Summary = "Updates an existing user", Description = "Requires authentication")]
        public IActionResult Update(int userId, [FromBody] UserRequest updatedUser)
        {
            try
            {
                var updatedUserInfo = this._userService.Update(userId, updatedUser);
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
            this._userService.Delete(userId);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all users", Description = "Requires authentication")]
        public IActionResult GetAllUsers()
        {
            var users = this._userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "Gets a certain user", Description = "Requires authentication")]
        public IActionResult GetUser(int userId)
        {
            var user = this._userService.GetById(userId);
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
 