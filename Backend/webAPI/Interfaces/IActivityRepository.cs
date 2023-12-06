using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface IActivityRepository
    {
        ActivityDataModel Create(ActivityDataModel newModel);
        ActivityDataModel Update(int activityDataId, ActivityDataModel updatedModel);
        void Delete(int activityDataId);
        List<ActivityDataModel> GetAll();
        List<ActivityDataModel> GetAllByUserId(int userId);
        ActivityDataModel GetById(int activityDataId);
    }
}
