using webApi.Data.Models;
using webAPI.Interfaces;

namespace webAPI.Services
{
    public class HealthRecommendationService
    {
        private readonly IHealthRecommendationRepository _healthRecommendationRepository;

        public HealthRecommendationService(IHealthRecommendationRepository healthRecommendationRepository)
        {
            _healthRecommendationRepository = healthRecommendationRepository;
        }

    }
}
