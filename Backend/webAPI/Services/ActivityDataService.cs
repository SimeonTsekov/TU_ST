using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.User;

namespace webAPI.Services
{
    public class ActivityDataService : IActivityDataService
    {
        private readonly IActivityDataRepository _activityRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ActivityDataService(IActivityDataRepository activityRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            this._activityRepository = activityRepository;
            this._currentUserService = currentUserService;
            this._mapper = mapper;
        }

        public ActivityResponse Create(ActivityRequest newModel)
        {
            var data = this._mapper.Map<ActivityDataModel>(newModel);

            data.UserId = this._currentUserService.GetCurrentUser().Id;

            var result = this._activityRepository.Create(data);

            return this._mapper.Map<ActivityResponse>(result);

        }

        public ActivityResponse Update(int id, ActivityRequest updatedModel)
        {
            var data = this._mapper.Map<ActivityDataModel>(updatedModel);
            var result = this._activityRepository.Update(id, data);
            return this._mapper.Map<ActivityResponse>(result);
        }

        public void Delete(int id)
        {
            this._activityRepository.Delete(id);
        }

        public List<ActivityResponse> Get(string order, int count)
        {
            return this._mapper.Map<List<ActivityResponse>>(this._activityRepository.Get(-1, order, count));
        }

        public ActivityResponse GetById(int id)
        {
            return this._mapper.Map<ActivityResponse>(this._activityRepository.GetById(id));
        }

        public List<ActivityResponse> GetByUserId(int userId, string order, int count)
        {
            return this._mapper.Map<List<ActivityResponse>>(this._activityRepository.Get(userId, order, count));
        }
    }
}
