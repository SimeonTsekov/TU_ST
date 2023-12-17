using webApi.Data.Models;

namespace webAPI.Interfaces.HealthData
{
    public interface IHealthDataRepository
    {
        HealthDataModel Create(HealthDataModel newHealthData);
        HealthDataModel Update(int healthDataId, HealthDataModel updatedHealthData);
        HealthDataModel GetHealthDataById(int healthDataId);
        List<HealthDataModel> Get(string order, int count);
        List<HealthDataModel> GetByUserId(int userId, string order, int count);

        HealthDataModel GetLatestHealthDataForTheCurrentUser();
		void Delete(int healthDataId);
    }
}
