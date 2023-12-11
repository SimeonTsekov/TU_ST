using webApi.Data.Models;

namespace webAPI.Interfaces.HealthRecommendation
{
    public interface IHealthRecommendationRepository
    {
		HealthRecommendationModel Create(HealthRecommendationModel newModel);
		List<HealthRecommendationModel> GetAllHealthRecommendationsDesc();
		HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId);
		List<HealthRecommendationModel> GetLastNRecommendations(int lastHealthRecommendationsNumber);
		void Delete(int healthRecommendationId);
	}
}
