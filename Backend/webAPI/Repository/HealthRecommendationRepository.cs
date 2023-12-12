using webAPI.Interfaces;
using webApi.Data.Models;
using webAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace webAPI.Repository
{
    public class HealthRecommendationRepository : IHealthRecommendationRepository
    {
        private readonly webAPIDbContext _dbContext;

        public HealthRecommendationRepository(webAPIDbContext dbContext)
        {
            _dbContext = dbContext;
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
            {
                return healthRecommendation;
            }

            throw new NullReferenceException();
        }
    }
}
