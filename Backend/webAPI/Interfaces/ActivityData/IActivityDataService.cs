using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRepository
{
    public interface IActivityDataService : ICrudService<ActivityRequest, ActivityResponse>
    {
        List<ActivityResponse> GetByUserId(int userId, string order, int count);
    }
}
