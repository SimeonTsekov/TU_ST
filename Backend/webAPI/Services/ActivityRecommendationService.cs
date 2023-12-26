using AutoMapper;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;
using webAPI.Interfaces.ActivityRecommendation;
using webAPI.Interfaces.ActivityRepository;
using webAPI.Interfaces.User;
using webAPI.Utils;

namespace webAPI.Services;

public class ActivityRecommendationService : IActivityRecommendationService
{
    private readonly IActivityRecommendationRepository _activityRecommendationRepository;
    private readonly IActivityDataRepository _activityRepository;
    private readonly IGPTService _gptService;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;    

    public ActivityRecommendationService(IActivityRecommendationRepository activityRecommendationRepository, 
        IActivityDataRepository activityRepository, IMapper mapper, IGPTService gptService, ICurrentUserService currentUserService)
    {
        this._activityRecommendationRepository = activityRecommendationRepository;
        this._activityRepository = activityRepository;
        this._gptService = gptService;
        this._currentUserService = currentUserService;
        this._mapper = mapper;
    }

    public async Task<RecommendationResponse> GenerateRecommendationAsync()
    {
        var activity = this._activityRepository.GetLatestActivityDataForTheCurrentUser();

        var prompt = "Based on this data, what would you recommend? " +
            $"Data: {activity.DailyDistance} daily distance, {activity.DailySteps} daily steps, {activity.Workouts} workouts for the day, {activity.DailyEnergyBurned} daily energy burned.";

        var result = await this._gptService.Ask(prompt);

        var recommendation = new ActivityRecommendationModel
        {
            Recommendation = result.Answer,
            UserId = activity.UserId
        };

        this._activityRecommendationRepository.Create(recommendation);

        return this._mapper.Map<RecommendationResponse>(recommendation);
    }

    public List<RecommendationResponse> GetActivityRecommendationsForTheCurrentUser(string order, int count)
    {
        return this._mapper.Map<List<RecommendationResponse>>(this._activityRecommendationRepository.Get(this._currentUserService.GetCurrentUser().Id, order, count));
    }

    public void Delete(int activityRecommendationId)
    {
        this._activityRecommendationRepository.Delete(activityRecommendationId);
    }

    public RecommendationResponse GetRecommendationById(int id)
    {
        return this._mapper.Map<RecommendationResponse>(this._activityRecommendationRepository.GetActivityRecommendationById(id));
    }

    public List<RecommendationResponse> Get(string order, int count)
    {
        return this._mapper.Map<List<RecommendationResponse>>(this._activityRecommendationRepository.Get(-1, order, count));
    }
}