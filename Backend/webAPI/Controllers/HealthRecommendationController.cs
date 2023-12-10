using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webAPI.Services;

namespace webAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthRecommendationController : ControllerBase
    {
        private readonly HealthRecommendationService _healthRecommendationService;

        public HealthRecommendationController(HealthRecommendationService healthRecommendationService)
        {
            _healthRecommendationService = healthRecommendationService;
        }
        
        [HttpGet("data")]
        [SwaggerOperation(Summary = "Gets health recommendation", Description = "Requires authentication")]
        public IActionResult GetActivityRecommendationData()
        {
            var result = this._healthRecommendationService.GenerateRecommendation();
            return Ok(result);
        }
    }
}
