using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRecommendation;

public interface IActivityRecommendationService
{
    Task<RecommendationResponse> GenerateRecommendationAsync();
    List<RecommendationResponse> GetLastNRecommendations(int lastActivityRecommendationsNumber);
    List<RecommendationResponse> GetLastRecommendationsDesc();
    RecommendationResponse GetRecommendationById(int id);
    void Delete(int activityRecommendationId);
}