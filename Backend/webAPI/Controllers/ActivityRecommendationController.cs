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
		[SwaggerOperation(Summary = "Gets activity recommendation", Description = "Requires authentication")]
		public IActionResult GetActivityRecommendationData()
		{
			var result = this._activityRecommendationService.GenerateRecommendation();
			return Ok(result);
		}
	}
}
