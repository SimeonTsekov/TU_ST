using webAPI.DTOs.Response;

namespace webAPI.Interfaces.HealthRecommendation;

public interface IHealthRecommendationService
{
	Task<RecommendationResponse> GenerateRecommendationAsync();
	RecommendationResponse GetRecommendationById(int id);
    List<RecommendationResponse> GetHealthRecommendationsForTheCurrentUser(string order, int count);
    List<RecommendationResponse> Get(string order, int count);
	void Delete(int healthRecommendationId);
}