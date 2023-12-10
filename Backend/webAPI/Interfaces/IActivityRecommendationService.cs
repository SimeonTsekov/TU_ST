using webAPI.DTOs.Response;

namespace webAPI.Interfaces;

public interface IActivityRecommendationService 
{
    RecommendationResponse GenerateRecommendation();
}