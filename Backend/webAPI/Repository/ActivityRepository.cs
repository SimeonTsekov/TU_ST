using AutoMapper;
using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces;

namespace webAPI.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly webAPIDbContext _dbContext;


        public ActivityRepository(webAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActivityDataModel Create(ActivityDataModel newModel)
        {
            _dbContext.ActivityDataModels.Add(newModel);
            _dbContext.SaveChanges();
            return newModel;
        }

        public ActivityDataModel Update(int activityDataId, ActivityDataModel updatedModel)
        {
            var existingModel = this.GetById(activityDataId);

            existingModel.DailyDistance = updatedModel.DailyDistance;
            existingModel.DailySteps = updatedModel.DailySteps;
            existingModel.DailyEnergyBurned = updatedModel.DailyEnergyBurned;
            existingModel.Workouts = updatedModel.Workouts;

            _dbContext.SaveChanges();
            return existingModel;
        }

        public void Delete(int activityDataId)
        {
            var modelToRemove = _dbContext.ActivityDataModels.Find(activityDataId);

            if (modelToRemove == null)
            {
                return;
            }

            _dbContext.ActivityDataModels.Remove(modelToRemove);
            _dbContext.SaveChanges();
        }

        public List<ActivityDataModel> GetAll()
        {
            return _dbContext.ActivityDataModels.ToList();
        }

        public List<ActivityDataModel> GetAllByUserId(int userId)
        {
            return _dbContext.ActivityDataModels
                .Where(a => a.UserId == userId)
                .ToList();
        }

        public ActivityDataModel GetById(int activityDataId)
        {
            return _dbContext.ActivityDataModels.Find(activityDataId) ?? throw new NullReferenceException("The activity with id '" + activityDataId + "' was not found.");
        }
    }
}
