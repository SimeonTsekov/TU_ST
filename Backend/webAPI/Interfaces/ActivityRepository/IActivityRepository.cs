using webApi.Data.Models;

namespace webAPI.Interfaces.ActivityRepository
{
    public interface IActivityRepository
    {
        ActivityDataModel Create(ActivityDataModel newModel);
        ActivityDataModel Update(int activityDataId, ActivityDataModel updatedModel);
        List<ActivityDataModel> GetAll();
        List<ActivityDataModel> GetAllByUserId(int userId);
        ActivityDataModel GetById(int activityDataId);
        ActivityDataModel GetLatestActivity();
        void Delete(int activityDataId);
    }
}
