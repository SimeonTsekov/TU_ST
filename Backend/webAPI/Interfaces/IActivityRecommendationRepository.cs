using webApi.Data.Models;

namespace webAPI.Interfaces
{
	public interface IActivityRecommendationRepository
	{
		ActivityRecommendationModel Create(ActivityRecommendationModel newModel);

		ActivityRecommendationModel Update(int activityRecommendationId, ActivityRecommendationModel updatedModel);

		void Delete(int activityRecommendationId);

		List<ActivityRecommendationModel> GetAllActivityRecommendations();

		ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId);
	}
}
