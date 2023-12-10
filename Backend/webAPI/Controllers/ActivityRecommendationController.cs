using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.Interfaces;

namespace webAPI.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ActivityRecommendationController : Controller
	{
		private readonly IActivityRecommendationService _activityRecommendationService;

        public ActivityRecommendationController(IActivityRecommendationService activityRecommendationService)
		{
			_activityRecommendationService = activityRecommendationService;
		}
		
		[HttpGet("data")]
		[SwaggerOperation(Summary = "Gets recommendation for the latest activity", Description = "Requires authentication")]
		public async Task<IActionResult> GetActivityRecommendationData()
		{
			var result = await this._activityRecommendationService.GenerateRecommendationAsync();
			return Ok(result);
		}

		[HttpGet("last/{lastActivityRecommendationsNumber}")]
		[SwaggerOperation(Summary = "Gets last N activity recommendations of the current user", Description = "Requires authentication")]
		public IActionResult GetLastNActivityRecommendations(int lastActivityRecommendationsNumber)
		{
			var result = this._activityRecommendationService.GetLastNRecommendations(lastActivityRecommendationsNumber);
			return Ok(result);
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Gets all activity recommendations of the current user in descending order", Description = "Requires authentication")]
		public IActionResult GetLastActivityRecommendations()
		{
			var result = this._activityRecommendationService.GetLastRecommendationsDesc();
			return Ok(result);
		}

		[HttpGet("{activityRecommendationId}")]
		[SwaggerOperation(Summary = "Gets activity recommendation by id", Description = "Requires authentication")]
		public IActionResult GetActivityRecommendationById(int activityRecommendationId)
		{
			try
			{
				var result = this._activityRecommendationService.GetRecommendationById(activityRecommendationId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{activityRecommendationId}")]
        [SwaggerOperation(Summary = "Deletes one recommendations of the current user", Description = "Requires authentication")]
        public IActionResult DeleteActivityRecommendations(int activityRecommendationId)
        {
			_activityRecommendationService.Delete(activityRecommendationId);
			return NoContent();
        }
    }
}
