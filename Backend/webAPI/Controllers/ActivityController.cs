using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new activity", Description = "Requires authentication")]
        public IActionResult Create([FromBody] ActivityRequest activityRequest)
        { 
            var user = (UserModel) HttpContext.Items["currentUser"]!;
            var result = this._activityService.Create(activityRequest, user);
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
        [SwaggerOperation(Summary = "Retrieves all activities", Description = "Requires authentication")]
        public IActionResult GetAllActivities()
        {
            var result = this._activityService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves activity by ID", Description = "Requires authentication")]
        public IActionResult GetActivity(int id)
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
