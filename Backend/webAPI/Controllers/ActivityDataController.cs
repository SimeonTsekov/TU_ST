using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.DTOs.Response;
using webAPI.Interfaces.ActivityRepository;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityDataService _activityService;

        public ActivityController(IActivityDataService activityService)
        {
            this._activityService = activityService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new activity for the current user", Description = "Requires authentication")]
        public IActionResult Create([FromBody] ActivityRequest activityRequest)
        { 
            var result = this._activityService.Create(activityRequest);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edits activity by ID", Description = "Requires authentication")]
        public IActionResult Update(int id, [FromBody] ActivityRequest activityRequest)
        {
            try
            {
                var result = this._activityService.Update(id, activityRequest);
                return Ok(result);
            }
            catch (System.Exception exception)
            {
                return NotFound(exception.Message);
            }  
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes activity by ID", Description = "Requires authentication")]
        public IActionResult Delete(int id)
        {
            this._activityService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves activities", Description = "Requires authentication")]
        public IActionResult GetAllActivitiesData(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var result = this._activityService.Get(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves activity by ID", Description = "Requires authentication")]
        public IActionResult GetActivityData(int id)
        {
            try
            {
                var result = this._activityService.GetById(id);
                return Ok(result);
            }
            catch (System.Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
