using webApi.Data.Models;

namespace webAPI.Interfaces.HealthRecommendation
{
    public interface IHealthRecommendationRepository
    {
		HealthRecommendationModel Create(HealthRecommendationModel newModel);
		HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId);
        List<HealthRecommendationModel> Get(int userId, string order, int count);
		void Delete(int healthRecommendationId);
	}
}
