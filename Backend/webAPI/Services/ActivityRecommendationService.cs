using webAPI.DTOs.Response;
using webAPI.Interfaces;

namespace webAPI.Services;

public class ActivityRecommendationService : IActivityRecommendationService
{
    private readonly IActivityRecommendationRepository _activityRecommendationRepository;

    public ActivityRecommendationService(IActivityRecommendationRepository activityRecommendationRepository)
    {
        _activityRecommendationRepository = activityRecommendationRepository;
    }

    public RecommendationResponse GenerateRecommendation()
    {
        throw new NotImplementedException();
    }
}