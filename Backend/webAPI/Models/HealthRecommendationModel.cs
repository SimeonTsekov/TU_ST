namespace webAPI.Models
{
	public class HealthRecommendationModel
	{
		public int HealthRecommendationId { get; set; }
		public int UserId { get; set; }
		public string DietaryAdvice { get; set; }
		public string SleepAdvice { get; set; }
		public string GeneralHealthAdvice { get; set; }

		// Navigation property to the User
		public virtual UserModel User { get; set; }
	}
}
