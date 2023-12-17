using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;
using webAPI.Interfaces.HealthData;
using webAPI.Interfaces.HealthRecommendation;

namespace webAPI.Services
{
    public class HealthRecommendationService : IHealthRecommendationService
    {
        private readonly IHealthRecommendationRepository _healthRecommendationRepository;
        private readonly IHealthDataRepository _healthRepository;
        private readonly IGPTService _gptService;
        private readonly IMapper _mapper;

		public HealthRecommendationService(IHealthRecommendationRepository healthRecommendationRepository,
			IHealthDataRepository healthRepository, IMapper mapper, IGPTService gptService)
        {
            this._healthRecommendationRepository = healthRecommendationRepository;
            this._healthRepository = healthRepository;
            this._gptService = gptService;
            this._mapper = mapper;
		}

        public void Delete(int healthRecommendationId)
		{
            this._healthRecommendationRepository.Delete(healthRecommendationId);
		}

		public async Task<RecommendationResponse> GenerateRecommendationAsync()
		{
			var healthData = this._healthRepository.GetLatestHealthDataForTheCurrentUser();

			var prompt = "Based on this data, what would you recommend? " +
				$"Data: {healthData.Bmi} BMI, {healthData.BodyFat} body fat, {healthData.BodyMass} body mass, {healthData.LeanBodyMass} lean body mass.";

			var result = await this._gptService.Ask(prompt);

			var recommendation = new HealthRecommendationModel
			{
				Recommendation = result.Answer,
				UserId = healthData.UserId
			};

			this._healthRecommendationRepository.Create(recommendation);

			return this._mapper.Map<RecommendationResponse>(recommendation);
		}

		public RecommendationResponse GetRecommendationById(int id)
		{
			return this._mapper.Map<RecommendationResponse>(this._healthRecommendationRepository.GetHealthRecommendationById(id));
		}

        public List<RecommendationResponse> GetHealthRecommendationsForTheCurrentUser(string order, int count)
        {
            return this._mapper.Map<List<RecommendationResponse>>(this._healthRecommendationRepository.GetHealthRecommendationsForTheCurrentUser(order, count));
        }

        public List<RecommendationResponse> Get(string order, int count)
        {
            return this._mapper.Map<List<RecommendationResponse>>(this._healthRecommendationRepository.Get(order, count));
        }
	}
}
