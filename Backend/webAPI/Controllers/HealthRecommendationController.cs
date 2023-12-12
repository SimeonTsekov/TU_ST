using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.Interfaces.HealthRecommendation;
using webAPI.Services;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthRecommendationController : Controller
    {
		private readonly IHealthRecommendationService _healthRecommendationService;

		public HealthRecommendationController(IHealthRecommendationService healthRecommendationService)
		{
            this._healthRecommendationService = healthRecommendationService;
		}

		[HttpGet("generate")]
		[SwaggerOperation(Summary = "Generates recommendation for the latest health state of the current user", Description = "Requires authentication")]
		public async Task<IActionResult> GenerateHealthRecommendation()
		{
            try
            {
                var result = await this._healthRecommendationService.GenerateRecommendationAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Retrieves health recommendations", Description = "Requires authentication")]
		public IActionResult GetHealthRecommendations(        
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
        {
            try
            {
                var result = this._healthRecommendationService.Get(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Gets health recommendation by id", Description = "Requires authentication")]
		public IActionResult GetHealthRecommendationById(int id)
		{
			try
			{
				var result = this._healthRecommendationService.GetRecommendationById(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(Summary = "Deletes one recommendation of the current user", Description = "Requires authentication")]
		public IActionResult DeleteHealthRecommendationById(int id)
		{
			this._healthRecommendationService.Delete(id);
			return NoContent();
		}
	}
}
