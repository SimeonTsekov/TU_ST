using webAPI.DTOs.Response;

namespace webAPI.Interfaces
{
    public interface IActivityService : IBaseService<ActivityRequest, ActivityResponse>
    {
        List<ActivityResponse> GetAllByUserId(int userId);
    }
}
