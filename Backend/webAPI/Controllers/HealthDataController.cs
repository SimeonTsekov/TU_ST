using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.DTOs.Request;
using webAPI.Interfaces.HealthData;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthDataController : Controller
    {
        private readonly IHealthDataService _healthDataService;

        public HealthDataController(IHealthDataService healthDataService)
        {
            this._healthDataService = healthDataService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates health data for the current user", Description = "Requires authentication")]
        public IActionResult Create([FromBody] HealthDataRequest healthDataRequest)
        {
            var result = this._healthDataService.Create(healthDataRequest);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Updates the health data by ID", Description = "Requires authentication")]
        public IActionResult Update(int id, [FromBody] HealthDataRequest healthDataRequest)
        {
            try
            {
                var result = this._healthDataService.Update(id, healthDataRequest);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes the health data by ID", Description = "Requires authentication")]
        public IActionResult Delete(int id)
        {
            this._healthDataService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Retrieves health data", Description = "Requires admin role")]
        public IActionResult GetHealthData(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var result = this._healthDataService.Get(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Gets specific health data by ID", Description = "Requires authentication")]
        public IActionResult GetHealthDataById(int id)
        {
            try
            {
                var result = this._healthDataService.GetById(id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
