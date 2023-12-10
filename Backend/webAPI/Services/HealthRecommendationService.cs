using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

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
