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
            _dbContext = dbContext;
            _currentUserService = currentUserService;
		}

        public HealthRecommendationModel Create(HealthRecommendationModel newRecommendation)
        {
            _dbContext.HealthRecommendationModels.Add(newRecommendation);
            _dbContext.SaveChanges();

            return newRecommendation;
        }

        public HealthRecommendationModel Update(int healthRecommendationId, HealthRecommendationModel updatedRecommendation)
        {
            var existingRecommendation = _dbContext.HealthRecommendationModels.Find(healthRecommendationId);

            if (existingRecommendation != null)
            {
                existingRecommendation.Recommendation = updatedRecommendation.Recommendation;

                _dbContext.SaveChanges();
            }

            return existingRecommendation!;
        }

        public void Delete(int healthRecommendationId)
        {
            var recommendationToRemove = _dbContext.HealthRecommendationModels.Find(healthRecommendationId);

            if (recommendationToRemove != null)
            {
                _dbContext.HealthRecommendationModels.Remove(recommendationToRemove);
                _dbContext.SaveChanges();
            }
        }

        public List<HealthRecommendationModel> GetAllHealthRecommendations()
        {
            return _dbContext.HealthRecommendationModels.ToList();
        }

        public HealthRecommendationModel GetHealthRecommendationById(int healthRecommendationId)
        {
            var healthRecommendation = _dbContext.HealthRecommendationModels.Find(healthRecommendationId);

            if (healthRecommendation != null)
                return healthRecommendation;

            throw new NullReferenceException();
        }

		public List<HealthRecommendationModel> GetAllHealthRecommendationsDesc()
		{
			var userId = _currentUserService.GetCurrentUser().Id;

			return _dbContext.HealthRecommendationModels
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.CreatedDate)
				.ToList();
		}

		public List<HealthRecommendationModel> GetLastNRecommendations(int lastHealthRecommendationsNumber)
		{
			var userId = _currentUserService.GetCurrentUser().Id;

			return _dbContext.HealthRecommendationModels
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.CreatedDate)
				.Take(lastHealthRecommendationsNumber)
				.ToList();
		}
	}
}
