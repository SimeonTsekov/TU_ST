using webApi.Data.Models;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRecommendation
{
    public interface IActivityRecommendationRepository
    {
        ActivityRecommendationModel Create(ActivityRecommendationModel newModel);
        ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId);
        List<ActivityRecommendationModel> Get(int userId, string order, int count);
        void Delete(int activityRecommendationId);
    }
}
