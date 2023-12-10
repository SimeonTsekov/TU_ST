using webAPI.Data;
using webApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Interfaces;
using webAPI.Utils;

namespace webAPI.Repository
{
	public class ActivityRecommendationRepository : IActivityRecommendationRepository
	{
		private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public ActivityRecommendationRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
		{
			_dbContext = dbContext;
			_currentUserService = currentUserService;
		}

		public ActivityRecommendationModel Create(ActivityRecommendationModel newModel)
		{
			_dbContext.ActivityRecommendationModels.Add(newModel);
			_dbContext.SaveChanges();

			return newModel;
		}

		public void Delete(int activityRecommendationId)
		{
			var modelToRemove = _dbContext.ActivityRecommendationModels.Find(activityRecommendationId);

			if (modelToRemove != null)
			{
				_dbContext.ActivityRecommendationModels.Remove(modelToRemove);
				_dbContext.SaveChanges();
			}
		}

		public ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId)
		{
			return _dbContext.ActivityRecommendationModels.Find(activityRecommendationId) ?? throw new NullReferenceException("The activity recommendation with id '" + activityRecommendationId + "' was not found.");
		}

		public List<ActivityRecommendationModel> GetAllActivityRecommendationsDesc()
		{
			var userId = _currentUserService.GetCurrentUser().Id;

			return _dbContext.ActivityRecommendationModels
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.CreatedDate)
				.ToList();
		}

		public List<ActivityRecommendationModel> GetLastNRecommendations(int lastActivityRecommendationsNumber)
		{
            var userId = _currentUserService.GetCurrentUser().Id;

			return _dbContext.ActivityRecommendationModels
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.CreatedDate)
				.Take(lastActivityRecommendationsNumber)
				.ToList();
        }
	}
}
