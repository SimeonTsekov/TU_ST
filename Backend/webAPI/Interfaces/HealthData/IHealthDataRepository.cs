using webApi.Data.Models;

namespace webAPI.Interfaces.HealthData
{
    public interface IHealthDataRepository
    {
        HealthDataModel Create(HealthDataModel newHealthData);
        HealthDataModel Update(int healthDataId, HealthDataModel updatedHealthData);
        HealthDataModel GetHealthDataById(int healthDataId);
        List<HealthDataModel> GetAllHealthData();
        List<HealthDataModel> GetAllHealthDataByUserId(int userId);
        void Delete(int healthDataId);
    }
}
