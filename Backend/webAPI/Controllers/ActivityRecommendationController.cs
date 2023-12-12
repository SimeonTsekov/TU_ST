using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.DTOs.Response;
using webAPI.Interfaces.ActivityRecommendation;

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
            this._activityRecommendationService = activityRecommendationService;
		}
		
		[HttpGet("generate")]
		[SwaggerOperation(Summary = "Generates recommendation for the latest activity of the current user", Description = "Requires authentication")]
		public async Task<IActionResult> GenerateActivityRecommendation()
		{
            try
            {
                var result = await this._activityRecommendationService.GenerateRecommendationAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Retrieves activity recommendations", Description = "Requires authentication")]
		public IActionResult GetActivityRecommendations(
            [FromQuery] [SwaggerParameter( Description = "The count of items to be returned. Use 0 for all items.", Required = false)] int count = 0,
            [FromQuery] [SwaggerParameter( Description = "The order of arrangement of items by date created. Possible values are 'asc' and 'desc'.", Required = false)] string order = "desc")
		{
            try
            {
                var result = this._activityRecommendationService.Get(order, count);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Conflict(exception.Message);
            }
		}

		[HttpGet("{id}")]
		[SwaggerOperation(Summary = "Gets activity recommendation by id", Description = "Requires authentication")]
		public IActionResult GetActivityRecommendationById(int id)
		{
			try
			{
				var result = this._activityRecommendationService.GetRecommendationById(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes recommendation by id", Description = "Requires authentication")]
        public IActionResult DeleteActivityRecommendationById(int id)
        {
			this._activityRecommendationService.Delete(id);
			return NoContent();
        }
    }
}
