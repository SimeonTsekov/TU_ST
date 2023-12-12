using webApi.Data.Models;

namespace webAPI.Interfaces
{
    public interface IHealthDataRepository
    {
        HealthDataModel Create(HealthDataModel newHealthData);
        HealthDataModel Update(int healthDataId, HealthDataModel updatedHealthData);
        void Delete(int healthDataId);
        List<HealthDataModel> GetAllHealthData();
        HealthDataModel GetHealthDataById(int healthDataId);
        List<HealthDataModel> GetAllHealthDataByUserId(int userId);
    }
}
