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
			_healthRecommendationService = healthRecommendationService;
		}

		[HttpGet("generate")]
		[SwaggerOperation(Summary = "Generates recommendation for the latest health state of the current user", Description = "Requires authentication")]
		public async Task<IActionResult> GetHealthRecommendationData()
		{
			var result = await this._healthRecommendationService.GenerateRecommendationAsync();
			return Ok(result);
		}

		[HttpGet("last/{lastHealthRecommendationsNumber}")]
		[SwaggerOperation(Summary = "Gets last N health recommendations of the current user", Description = "Requires authentication")]
		public IActionResult GetLastNHealthRecommendations(int lastHealthRecommendationsNumber)
		{
			var result = this._healthRecommendationService.GetLastNRecommendations(lastHealthRecommendationsNumber);
			return Ok(result);
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Gets all health recommendations of the current user in descending order by date created", Description = "Requires authentication")]
		public IActionResult GetLastActivityRecommendations()
		{
			var result = this._healthRecommendationService.GetLastRecommendationsDesc();
			return Ok(result);
		}

		[HttpGet("{healthRecommendationId}")]
		[SwaggerOperation(Summary = "Gets health recommendation by id", Description = "Requires authentication")]
		public IActionResult GetHealthRecommendationById(int healthRecommendationId)
		{
			try
			{
				var result = this._healthRecommendationService.GetRecommendationById(healthRecommendationId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{healthRecommendationId}")]
		[SwaggerOperation(Summary = "Deletes one recommendation of the current user", Description = "Requires authentication")]
		public IActionResult DeleteHealthRecommendations(int healthRecommendationId)
		{
			_healthRecommendationService.Delete(healthRecommendationId);
			return NoContent();
		}
	}
}
