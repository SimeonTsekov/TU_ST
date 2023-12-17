using webApi.Data.Models;

namespace webAPI.Interfaces.HealthRecommendation
{
    public interface IHealthRecommendationRepository
    {
		HealthRecommendationModel Create(HealthRecommendationModel newModel);
		HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId);
        List<HealthRecommendationModel> GetHealthRecommendationsForTheCurrentUser(string order, int count);
        List<HealthRecommendationModel> Get(string order, int count);
		void Delete(int healthRecommendationId);
	}
}
