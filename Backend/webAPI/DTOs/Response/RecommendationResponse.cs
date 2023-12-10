namespace webAPI.DTOs.Response;

public class RecommendationResponse
{
    public int Id { get; set; }
    public string? Recommendation { get; set; }
    public DateTime CreatedDate { get; set; }
}