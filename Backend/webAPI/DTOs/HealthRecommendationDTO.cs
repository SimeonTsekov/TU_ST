namespace webAPI.DTOs
{
	public class HealthRecommendationDTO
	{
		public int HealthRecommendationId { get; set; }
		public int UserId { get; set; }
		public string? DietaryAdvice { get; set; }
		public string? SleepAdvice { get; set; }
		public string? GeneralHealthAdvice { get; set; }

		// Navigation property to the User
		public virtual UserDTO? User { get; set; }
	}
}
