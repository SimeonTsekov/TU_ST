using webAPI.DTOs.Response;

namespace webAPI.Interfaces;

public interface IHealthRecommendationService
{
    RecommendationResponse GenerateRecommendation();
}