using webAPI.DTOs.Request;
using webAPI.DTOs.Response;

namespace webAPI.Interfaces.HealthData
{
    public interface IHealthDataService : IBaseService<HealthDataRequest, HealthDataResponse>
    {
        List<HealthDataResponse> GetAllByUserId(int userId);
    }
}
