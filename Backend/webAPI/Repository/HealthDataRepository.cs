using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces.HealthData;

namespace webAPI.Repository
{
    public class HealthDataRepository : IHealthDataRepository
    {
        private readonly webAPIDbContext _dbContext;

        public HealthDataRepository(webAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public HealthDataModel Create(HealthDataModel newHealthData)
        {
            _dbContext.HealthDataModels.Add(newHealthData);
            _dbContext.SaveChanges();

            return newHealthData;
        }

        public HealthDataModel Update(int healthDataId, HealthDataModel updatedHealthData)
        {
            var existingData = GetHealthDataById(healthDataId);

            if (existingData != null)
            {
                existingData.BodyMass = updatedHealthData.BodyMass;
                existingData.Bmi = updatedHealthData.Bmi;
                existingData.BodyFat = updatedHealthData.BodyFat;
                existingData.LeanBodyMass = updatedHealthData.LeanBodyMass;
                existingData.SleepAnalysis = updatedHealthData.SleepAnalysis;
            }

            _dbContext.SaveChanges();

            return existingData!;
        }

        public void Delete(int healthDataId)
        {
            var modelToRemove = _dbContext.HealthDataModels.Find(healthDataId);

            if (modelToRemove == null)
                return;

            _dbContext.HealthDataModels.Remove(modelToRemove);
            _dbContext.SaveChanges();
        }

        public List<HealthDataModel> GetAllHealthData()
        {
            return _dbContext.HealthDataModels.ToList();
        }

        public List<HealthDataModel> GetAllHealthDataByUserId(int userId)
        {
            return _dbContext.HealthDataModels.Where(u => u.UserId == userId).ToList();
        }

        public HealthDataModel GetHealthDataById(int healthDataId)
        {
            return _dbContext.HealthDataModels.Find(healthDataId) ?? throw new NullReferenceException("The health data with id '" + healthDataId + "' was not found.");
        }
    }
}
