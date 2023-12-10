using AutoMapper;
using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces;
using webAPI.Utils;

namespace webAPI.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;


        public ActivityRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
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

        public ActivityDataModel GetLatestActivity()
        {
            var userId = _currentUserService.GetCurrentUser().Id;

            return _dbContext.ActivityDataModels
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefault() ?? throw new NullReferenceException("There are no activities for the specified user in the database!");
        }
    }
}
