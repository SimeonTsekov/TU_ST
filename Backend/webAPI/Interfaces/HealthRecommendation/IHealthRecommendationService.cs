using webAPI.DTOs.Response;

namespace webAPI.Interfaces.HealthRecommendation;

public interface IHealthRecommendationService
{
    RecommendationResponse GenerateRecommendation();
}