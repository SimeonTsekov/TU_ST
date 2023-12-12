using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.HealthData
{
    public interface IHealthDataService : ICrudService<HealthDataRequest, HealthDataResponse>
    {
        List<HealthDataResponse> GetByUserId(int userId, string order, int count);
    }
}
