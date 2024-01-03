using webApi.Data.Models;
using webAPI.Data;
using webAPI.Interfaces.HealthRecommendation;
using webAPI.Interfaces.User;

namespace webAPI.Repository
{
    public class HealthRecommendationRepository : IHealthRecommendationRepository
    {
        private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

		public HealthRecommendationRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
        {
            this._dbContext = dbContext;
            this._currentUserService = currentUserService;
		}

        public HealthRecommendationModel Create(HealthRecommendationModel newRecommendation)
        {
            this._dbContext.HealthRecommendationModels.Add(newRecommendation);
            this._dbContext.SaveChanges();
            return newRecommendation;
        }

        public void Delete(int healthRecommendationId)
        {
            var recommendationToRemove = _dbContext.HealthRecommendationModels.Find(healthRecommendationId);

            if (recommendationToRemove == null)
            {
                return;
            }

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != recommendationToRemove.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            this._dbContext.HealthRecommendationModels.Remove(recommendationToRemove);
            this._dbContext.SaveChanges();
        }

        public HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId)
        {
            var recommendation = this._dbContext.HealthRecommendationModels.Find(healthRecommendationId) ?? throw new NullReferenceException("The health recommendation with id '" + healthRecommendationId + "' was not found.");

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != recommendation.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            return recommendation;
        }

        public List<HealthRecommendationModel> Get(int userId, string order, int count)
        {
            var query = userId > 0 ? this._dbContext.HealthRecommendationModels.Where(a => a.UserId == userId)
                : this._dbContext.HealthRecommendationModels.AsQueryable();

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
