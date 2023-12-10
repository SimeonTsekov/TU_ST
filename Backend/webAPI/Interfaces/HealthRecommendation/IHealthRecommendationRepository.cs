using webApi.Data.Models;

namespace webAPI.Interfaces.HealthRecommendation
{
    public interface IHealthRecommendationRepository
    {
        HealthRecommendationModel Create(HealthRecommendationModel newRecommendation);
        HealthRecommendationModel Update(int healthRecommendationId, HealthRecommendationModel updatedRecommendation);
        HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId);
        List<HealthRecommendationModel> GetAllHealthRecommendations();
        void Delete(int healthRecommendationId);
    }
}
