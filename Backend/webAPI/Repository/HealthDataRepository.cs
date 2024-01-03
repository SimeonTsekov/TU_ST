using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces.HealthData;
using webAPI.Interfaces.User;

namespace webAPI.Repository
{
    public class HealthDataRepository : IHealthDataRepository
    {
        private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

		public HealthDataRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
        {
            this._dbContext = dbContext;
            _currentUserService = currentUserService;
		}

        public HealthDataModel Create(HealthDataModel newHealthData)
        {
            this._dbContext.HealthDataModels.Add(newHealthData);
            this._dbContext.SaveChanges();
            return newHealthData;
        }

        public HealthDataModel Update(int healthDataId, HealthDataModel updatedHealthData)
        {
            var existingData = GetHealthDataById(healthDataId);

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != existingData.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            existingData.BodyMass = updatedHealthData.BodyMass;
            existingData.Bmi = updatedHealthData.Bmi;
            existingData.BodyFat = updatedHealthData.BodyFat;
            existingData.LeanBodyMass = updatedHealthData.LeanBodyMass;

            this._dbContext.SaveChanges();

            return existingData!;
        }

        public void Delete(int healthDataId)
        {
            var modelToRemove = _dbContext.HealthDataModels.Find(healthDataId);

            if (modelToRemove == null)
            {
                return;
            }

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != modelToRemove.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            this._dbContext.HealthDataModels.Remove(modelToRemove);
            this._dbContext.SaveChanges();
        }

        public List<HealthDataModel> Get(int userId, string order, int count)
        {
            var query = userId > 0 ? this._dbContext.HealthDataModels.Where(a => a.UserId == userId)
                : this._dbContext.HealthDataModels.AsQueryable();

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

        public HealthDataModel GetHealthDataById(int healthDataId)
        {
            var healthData = this._dbContext.HealthDataModels.Find(healthDataId) ?? throw new NullReferenceException("The health data with id '" + healthDataId + "' was not found.");

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != healthData.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            return healthData;
        }

		public HealthDataModel GetLatestHealthDataForTheCurrentUser()
		{
			var userId = _currentUserService.GetCurrentUser().Id;

			return this._dbContext.HealthDataModels
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.CreatedDate)
				.FirstOrDefault() ?? throw new NullReferenceException("There are no health data for the specified user in the database!");
		}
	}
}
