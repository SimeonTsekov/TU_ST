using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using OpenAI_API;
using OpenAI_API.Completions;
using webApi.Data.Models;
using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Services;

public class ActivityRecommendationService : IActivityRecommendationService
{
    private readonly IActivityRecommendationRepository _activityRecommendationRepository;
    private readonly IActivityRepository _activityRepository;
    private readonly IGPTService _gptService;
    private readonly IMapper _mapper;

    public ActivityRecommendationService(IActivityRecommendationRepository activityRecommendationRepository, 
        IActivityRepository activityRepository, IMapper mapper, IGPTService gptService)
    {
        _activityRecommendationRepository = activityRecommendationRepository;
        _activityRepository = activityRepository;
        _gptService = gptService;
        _mapper = mapper;
    }

    public async Task<RecommendationResponse> GenerateRecommendationAsync()
    {
        var activity = this._activityRepository.GetLatestActivity();

        var prompt = "Based on this data, what would you recommend? " +
            $"Data: {activity.DailyDistance} daily distance, {activity.DailySteps} daily steps, {activity.Workouts} workouts for the day, {activity.DailyEnergyBurned} daily energy burned.";

        var result = await this._gptService.Ask(prompt);

        var recommendation = new ActivityRecommendationModel
        {
            Recommendation = result.Text,
            UserId = activity.UserId
        };

        this._activityRecommendationRepository.Create(recommendation);

        return this._mapper.Map<RecommendationResponse>(recommendation);
    }

    public List<RecommendationResponse> GetLastNRecommendations(int lastActivityRecommendationsNumber)
    {
        return this._mapper.Map<List<RecommendationResponse>>(this._activityRecommendationRepository.GetLastNRecommendations(lastActivityRecommendationsNumber));
    }

    public List<RecommendationResponse> GetLastRecommendationsDesc()
    {
        return this._mapper.Map<List<RecommendationResponse>>(this._activityRecommendationRepository.GetAllActivityRecommendationsDesc());
    }

    public void Delete(int activityRecommendationId)
    {
        _activityRecommendationRepository.Delete(activityRecommendationId);
    }

    public RecommendationResponse GetRecommendationById(int id)
    {
        return this._mapper.Map<RecommendationResponse>(this._activityRecommendationRepository.GetActivityRecommendationById(id));
    }
}