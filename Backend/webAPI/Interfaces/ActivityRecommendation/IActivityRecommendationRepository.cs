using webApi.Data.Models;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRecommendation
{
    public interface IActivityRecommendationRepository
    {
        ActivityRecommendationModel Create(ActivityRecommendationModel newModel);
        ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId);
        List<ActivityRecommendationModel> GetActivityRecommendationsForTheCurrentUser(string order, int count);
        List<ActivityRecommendationModel> Get(string order, int count);
        void Delete(int activityRecommendationId);
    }
}
