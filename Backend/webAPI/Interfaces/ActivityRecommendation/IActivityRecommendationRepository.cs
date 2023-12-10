using webApi.Data.Models;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRecommendation
{
    public interface IActivityRecommendationRepository
    {
        ActivityRecommendationModel Create(ActivityRecommendationModel newModel);
        List<ActivityRecommendationModel> GetAllActivityRecommendationsDesc();
        ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId);
        List<ActivityRecommendationModel> GetLastNRecommendations(int lastActivityRecommendationsNumber);
        void Delete(int activityRecommendationId);
    }
}
