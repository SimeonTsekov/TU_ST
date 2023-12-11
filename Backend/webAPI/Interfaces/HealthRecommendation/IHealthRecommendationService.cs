using webAPI.DTOs.Response;

namespace webAPI.Interfaces.HealthRecommendation;

public interface IHealthRecommendationService
{
	Task<RecommendationResponse> GenerateRecommendationAsync();
	List<RecommendationResponse> GetLastNRecommendations(int lastHealthRecommendationsNumber);
	List<RecommendationResponse> GetLastRecommendationsDesc();
	RecommendationResponse GetRecommendationById(int id);
	void Delete(int healthRecommendationId);
}