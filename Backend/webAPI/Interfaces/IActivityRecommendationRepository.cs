using webApi.Data.Models;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces
{
	public interface IActivityRecommendationRepository
	{
		ActivityRecommendationModel Create(ActivityRecommendationModel newModel);

		void Delete(int activityRecommendationId);

		List<ActivityRecommendationModel> GetAllActivityRecommendationsDesc();

		ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId);

		List<ActivityRecommendationModel> GetLastNRecommendations(int lastActivityRecommendationsNumber);
	}
}
