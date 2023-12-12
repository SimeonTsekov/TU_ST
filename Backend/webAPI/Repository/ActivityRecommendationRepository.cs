using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces.ActivityRecommendation;
using webAPI.Interfaces.User;

namespace webAPI.Repository
{
    public class ActivityRecommendationRepository : IActivityRecommendationRepository
	{
		private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public ActivityRecommendationRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
		{
			this._dbContext = dbContext;
			_currentUserService = currentUserService;
		}

		public ActivityRecommendationModel Create(ActivityRecommendationModel newModel)
		{
            this._dbContext.ActivityRecommendationModels.Add(newModel);
            this._dbContext.SaveChanges();
			return newModel;
		}

        public void Delete(int activityRecommendationId)
		{
			var modelToRemove = this._dbContext.ActivityRecommendationModels.Find(activityRecommendationId);

            if (modelToRemove == null)
            {
                return;
            }

            this._dbContext.ActivityRecommendationModels.Remove(modelToRemove);
            this._dbContext.SaveChanges();
        }

		public ActivityRecommendationModel GetActivityRecommendationById(int activityRecommendationId)
		{
			return this._dbContext.ActivityRecommendationModels.Find(activityRecommendationId) ?? throw new NullReferenceException("The activity recommendation with id '" + activityRecommendationId + "' was not found.");
		}

        public List<ActivityRecommendationModel> GetActivityRecommendationsForTheCurrentUser(string order, int count)
        {
            var userId = _currentUserService.GetCurrentUser().Id;
            var query = this._dbContext.ActivityRecommendationModels.Where(a => a.UserId == userId);

            query = order.ToLower() switch
            {
                "asc" => query.OrderBy(a => a.CreatedDate),
                "desc" => query.OrderByDescending(a => a.CreatedDate),
                _ => throw new ArgumentException("Invalid order parameter. Accepted values are 'asc' or 'desc'.")
            };

            if (count > 0)
            {
                query = query.Take(count);
            }

            return query.ToList();
        }

        public List<ActivityRecommendationModel> Get(string order, int count)
        {
            var query = this._dbContext.ActivityRecommendationModels.AsQueryable();

            query = order.ToLower() switch
            {
                "asc" => query.OrderBy(a => a.CreatedDate),
                "desc" => query.OrderByDescending(a => a.CreatedDate),
                _ => throw new ArgumentException("Invalid order parameter. Accepted values are 'asc' or 'desc'.")
            };

            if (count > 0)
            {
                query = query.Take(count);
            }

            return query.ToList();
        }
	}
}
