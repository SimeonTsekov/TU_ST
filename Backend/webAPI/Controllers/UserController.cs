using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using webAPI.DTOs.Request;
using webAPI.Interfaces.ActivityRecommendation;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.HealthData;
using webAPI.Interfaces.HealthRecommendation;
using webAPI.Interfaces.User;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IActivityDataService _activityService;
        private readonly IHealthDataService _healthService;
        private readonly IUserService _userService;
        private readonly IActivityRecommendationService _activityRecommendationService;
        private readonly IHealthRecommendationService _healthRecommendationService;
        private readonly int _currentUserId;

        public UserController(IActivityDataService activityService, IHealthDataService healthService, 
            IUserService userService, ICurrentUserService currentUserService, IActivityRecommendationService activityRecommendationService,
            IHealthRecommendationService healthRecommendationService)
        {
            this._activityService = activityService;
            this._healthService = healthService;
            this._userService = userService;
            this._activityRecommendationService = activityRecommendationService;
            this._healthRecommendationService = healthRecommendationService;
            this._currentUserId = currentUserService.GetCurrentUser().Id;
        }

        [HttpPut()]
        [SwaggerOperation(Summary = "Updates the current user", Description = "Requires authentication")]
        public IActionResult Update([FromBody] UserRequest updatedUser)
        {
            try
            {
                var updatedUserInfo = this._userService.Update(this._currentUserId, updatedUser);
                return Ok(updatedUserInfo);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete()]
        [SwaggerOperation(Summary = "Deletes the current user", Description = "Requires authentication")]
        public IActionResult Delete()
        {
            this._userService.Delete(this._currentUserId);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves users", Description = "Requires authentication")]
        public IActionResult GetUsers(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var users = this._userService.Get(order, count);
                return Ok(users);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("current-user")]
        [SwaggerOperation(Summary = "Returns the current user", Description = "Requires authentication")]
        public IActionResult GetUser()
        {
            var user = this._userService.GetById(this._currentUserId);
            return Ok(user);
        }

        [HttpGet("activityData")]
        [SwaggerOperation(Summary = "Retrieves activities of the current user", Description = "Requires authentication")]
        public IActionResult GetActivitiesForUser(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var activityDataById = this._activityService.GetByUserId(this._currentUserId, order, count);
                return Ok(activityDataById);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("healthData")]
        [SwaggerOperation(Summary = "Retrieves health data of the current user", Description = "Requires authentication")]
        public IActionResult GetHealthDataForUser(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var healthDataById = this._healthService.GetByUserId(this._currentUserId, order, count);
                return Ok(healthDataById);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet("activityRecommendations")]
        [SwaggerOperation(Summary = "Retrieves activity recommendations of the current user", Description = "Requires authentication")]
        public IActionResult GetActivityRecommendations(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var result = this._activityRecommendationService.GetActivityRecommendationsForTheCurrentUser(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }

        [HttpGet("healthRecommendations")]
        [SwaggerOperation(Summary = "Retrieves health recommendations of the current user", Description = "Requires authentication")]
        public IActionResult GetHealthRecommendations(        
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var result = this._healthRecommendationService.GetHealthRecommendationsForTheCurrentUser(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }
    }
}
 