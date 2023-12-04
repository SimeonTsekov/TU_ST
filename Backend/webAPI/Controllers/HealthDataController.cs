using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApi.Data.Models;
using webAPI.DTOs.Request;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

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
            _healthDataService = healthDataService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates health data for the current user", Description = "Requires authentication")]
        public IActionResult Create([FromBody] HealthDataRequest healthDataRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = (UserModel)HttpContext.Items["currentUser"]!;
            var result = this._healthDataService.Create(healthDataRequest, user);

            return Ok(result);
        }

        [HttpPut("{healthDataId}")]
        [SwaggerOperation(Summary = "Updates the health data for the current user", Description = "Requires authentication")]
        public IActionResult Update(int healthDataId, [FromBody] HealthDataRequest healthDataRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = this._healthDataService.Update(healthDataId, healthDataRequest);
                return Ok(result);
            }
            catch (System.Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpDelete("{healthDataId}")]
        [SwaggerOperation(Summary = "Deletes the health data for the current user", Description = "Requires authentication")]
        public IActionResult Delete(int healthDataId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this._healthDataService.Delete(healthDataId);

            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Gets all the health data for every user", Description = "Requires authentication")]
        public IActionResult GetAllHealthData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = this._healthDataService.GetAll();

            return Ok(result);
        }

        [HttpGet("{healthDataId}")]
        [SwaggerOperation(Summary = "Gets spevific health data for the specific user", Description = "Requires authentication")]
        public IActionResult GetHealthDataById(int healthDataId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = this._healthDataService.GetById(healthDataId);
                return Ok(result);
            }
            catch (System.Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
