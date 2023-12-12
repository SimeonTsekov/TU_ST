using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRecommendation;

public interface IActivityRecommendationService
{
    Task<RecommendationResponse> GenerateRecommendationAsync();
    RecommendationResponse GetRecommendationById(int id);
    List<RecommendationResponse> GetActivityRecommendationsForTheCurrentUser(string order, int count);
    List<RecommendationResponse> Get(string order, int count);
    void Delete(int activityRecommendationId);
}