using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs.Request;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Services
{
    public class HealthDataService : IHealthDataService
    {
        private readonly IHealthDataRepository _healthDataRepository;
        private readonly IMapper _mapper;

        public HealthDataService(IHealthDataRepository healthDataRepository, IMapper mapper)
        {
            _healthDataRepository = healthDataRepository;
            _mapper = mapper;
        }

        public HealthDataResponse Create(HealthDataRequest newModel, UserModel user)
        {
            var data = this._mapper.Map<HealthDataModel>(newModel);

            data.UserId = user.UserId;
            // data.UserModel = user;

            var result = this._healthDataRepository.Create(data);

            return this._mapper.Map<HealthDataResponse>(result);
        }

        public HealthDataResponse Update(int id, HealthDataRequest updatedModel)
        {
            var data = this._mapper.Map<HealthDataModel>(updatedModel);
            var result = this._healthDataRepository.Update(id, data);

            return this._mapper.Map<HealthDataResponse>(result);
        }

        public void Delete(int id)
        {
            this._healthDataRepository.Delete(id);
        }

        public List<HealthDataResponse> GetAll()
        {
            return this._mapper.Map<List<HealthDataResponse>>(this._healthDataRepository.GetAllHealthData());
        }

        public HealthDataResponse GetById(int id)
        {
            return this._mapper.Map<HealthDataResponse>(this._healthDataRepository.GetHealthDataById(id));
        }

        public List<HealthDataResponse> GetAllByUserId(int userId)
        {
            return this._mapper.Map<List<HealthDataResponse>>(this._healthDataRepository.GetAllHealthDataByUserId(userId));
        }
    }
}
