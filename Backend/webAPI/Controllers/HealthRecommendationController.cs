using Microsoft.AspNetCore.Mvc;
using webApi.Data.Models;
using webAPI.Services;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthRecommendationController : ControllerBase
    {
        private readonly HealthRecommendationService _healthRecommendationService;

        public HealthRecommendationController(HealthRecommendationService healthRecommendationService)
        {
            _healthRecommendationService = healthRecommendationService;
        }
    }
}
