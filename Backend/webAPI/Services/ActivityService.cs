using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _activityRepository = activityRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
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

        public List<ActivityResponse> GetAll()
        {
            return this._mapper.Map<List<ActivityResponse>>(this._activityRepository.GetAll());
        }

        public ActivityResponse GetById(int id)
        {
            return this._mapper.Map<ActivityResponse>(this._activityRepository.GetById(id));
        }

        public List<ActivityResponse> GetAllByUserId(int userId)
        {
            return this._mapper.Map<List<ActivityResponse>>(this._activityRepository.GetAllByUserId(userId));
        }
    }
}
