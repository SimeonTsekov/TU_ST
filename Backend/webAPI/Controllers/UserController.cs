using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.DTOs.Request;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.HealthData;
using webAPI.Interfaces.User;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IHealthDataService _healthService;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IActivityService activityService, IHealthDataService healthService, 
            IUserService userService, ICurrentUserService currentUserService)
        {
            _activityService = activityService;
            _healthService = healthService;
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPut()]
        [SwaggerOperation(Summary = "Updates the current user", Description = "Requires authentication")]
        public IActionResult Update([FromBody] UserRequest updatedUser)
        {
            try
            {
                var userId = this._currentUserService.GetCurrentUser().Id;
                var updatedUserInfo = this._userService.Update(userId, updatedUser);
                return Ok(updatedUserInfo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user data");
            }
        }

        [HttpDelete()]
        [SwaggerOperation(Summary = "Deletes the current user", Description = "Requires authentication")]
        public IActionResult Delete()
        {
            this._userService.Delete(_currentUserService.GetCurrentUser().Id);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all users", Description = "Requires authentication")]
        public IActionResult GetAllUsers()
        {
            var users = this._userService.GetAll();
            return Ok(users);
        }

        [HttpGet("current-user")]
        [SwaggerOperation(Summary = "Returns the current user", Description = "Requires authentication")]
        public IActionResult GetUser()
        {
            var user = this._userService.GetById(_currentUserService.GetCurrentUser().Id);
            return Ok(user);
        }

        [HttpGet("activities")]
        [SwaggerOperation(Summary = "Gets activities of the current user", Description = "Requires authentication")]
        public IActionResult GetActivitiesForUser()
        {
            var activityDataById = this._activityService.GetAllByUserId(_currentUserService.GetCurrentUser().Id);
            return Ok(activityDataById);
        }

        [HttpGet("healthData")]
        [SwaggerOperation(Summary = "Gets health data of the current user", Description = "Requires authentication")]
        public IActionResult GetHealthDataForUser()
        {
            var healthDataById = _healthService.GetAllByUserId(_currentUserService.GetCurrentUser().Id);
            return Ok(healthDataById);
        }
    }
}
 