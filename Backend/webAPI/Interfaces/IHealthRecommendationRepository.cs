using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface IHealthRecommendationRepository
    {
        HealthRecommendationModel Create(HealthRecommendationModel newRecommendation);

        HealthRecommendationModel Update(int healthRecommendationId, HealthRecommendationModel updatedRecommendation);

        void Delete(int healthRecommendationId);

        List<HealthRecommendationModel> GetAllHealthRecommendations();

        HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId);
    }
}
