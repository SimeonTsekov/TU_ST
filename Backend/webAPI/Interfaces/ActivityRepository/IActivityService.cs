using webAPI.DTOs.Response;

namespace webAPI.Interfaces.ActivityRepository
{
    public interface IActivityService : IBaseService<ActivityRequest, ActivityResponse>
    {
        List<ActivityResponse> GetAllByUserId(int userId);
    }
}
