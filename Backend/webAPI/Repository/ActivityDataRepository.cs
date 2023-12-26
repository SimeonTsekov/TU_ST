using webAPI.Data;
using webApi.Data.Models;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.User;

namespace webAPI.Repository
{
    public class ActivityDataRepository : IActivityDataRepository
    {
        private readonly webAPIDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;


        public ActivityDataRepository(webAPIDbContext dbContext, ICurrentUserService currentUserService)
        {
            this._dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public ActivityDataModel Create(ActivityDataModel newModel)
        {
            this._dbContext.ActivityDataModels.Add(newModel);
            this._dbContext.SaveChanges();
            return newModel;
        }

        public ActivityDataModel Update(int activityDataId, ActivityDataModel updatedModel)
        {
            var existingModel = this.GetById(activityDataId);

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != existingModel.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            existingModel.DailyDistance = updatedModel.DailyDistance;
            existingModel.DailySteps = updatedModel.DailySteps;
            existingModel.DailyEnergyBurned = updatedModel.DailyEnergyBurned;
            existingModel.Workouts = updatedModel.Workouts;

            this._dbContext.SaveChanges();
            return existingModel;
        }

        public void Delete(int activityDataId)
        {
            var modelToRemove = this._dbContext.ActivityDataModels.Find(activityDataId);

            if (modelToRemove == null)
            {
                return;
            }

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != modelToRemove.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            this._dbContext.ActivityDataModels.Remove(modelToRemove);
            this._dbContext.SaveChanges();
        }

        public List<ActivityDataModel> Get(int userId, string order, int count)
        {
            var query = userId > 0 ? this._dbContext.ActivityDataModels.Where(a => a.UserId == userId) : this._dbContext.ActivityDataModels.AsQueryable();

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

        public ActivityDataModel GetById(int activityDataId)
        {
            var activity = this._dbContext.ActivityDataModels.Find(activityDataId) ?? throw new NullReferenceException("The activity with id '" + activityDataId + "' was not found."); ;

            if (!_currentUserService.IsAdmin() && _currentUserService.GetCurrentUser().Id != activity.UserId)
            {
                throw new InvalidOperationException("You do not have access to this resource!");
            }

            return activity;
        }

        public ActivityDataModel GetLatestActivityDataForTheCurrentUser()
        {
            var userId = _currentUserService.GetCurrentUser().Id;

            return this._dbContext.ActivityDataModels
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefault() ?? throw new NullReferenceException("There are no activities for the specified user in the database!");
        }
    }
}
