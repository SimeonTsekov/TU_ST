using webAPI.DTOs.Response;
using webAPI.Interfaces.HealthRecommendation;

namespace webAPI.Services
{
    public class HealthRecommendationService : IHealthRecommendationService
    {
        private readonly IHealthRecommendationRepository _healthRecommendationRepository;

        public HealthRecommendationService(IHealthRecommendationRepository healthRecommendationRepository)
        {
            _healthRecommendationRepository = healthRecommendationRepository;
        }

        public RecommendationResponse GenerateRecommendation()
        {
            throw new NotImplementedException();
        }
    }
}
