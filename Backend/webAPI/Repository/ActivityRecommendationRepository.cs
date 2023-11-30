using webAPI.Data;
using webApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using webAPI.Interfaces;

namespace webAPI.Repository
{
	public class ActivityRecommendationRepository : IActivityRecommendationRepository
	{
		private readonly webAPIDbContext _dbContext;

		public ActivityRecommendationRepository(webAPIDbContext dbContext)
		{
			_dbContext = dbContext;
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
			var activityRecommendation = _dbContext.ActivityRecommendationModels.Find(activityRecommendationId);

			if (activityRecommendation != null)
			{
				return activityRecommendation;
			}

			throw new NullReferenceException();
		}

		public List<ActivityRecommendationModel> GetAllActivityRecommendations()
		{
			return _dbContext.ActivityRecommendationModels.ToList();
		}

		public ActivityRecommendationModel Update(int activityRecommendationId, ActivityRecommendationModel updatedModel)
		{
			var existingModel = _dbContext.ActivityRecommendationModels.Find(activityRecommendationId);

			if (existingModel != null)
			{
				existingModel.WorkoutRecommendations = updatedModel.WorkoutRecommendations;
				existingModel.ActivityGoals = updatedModel.ActivityGoals;
				existingModel.CustomActivityAdvice = updatedModel.CustomActivityAdvice;
				existingModel.UserModel = updatedModel.UserModel;
			}

			return existingModel;
		}
	}
}
