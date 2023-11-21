namespace webAPI.Models
{
	public class ActivityRecommendationModel
	{
		public int ActivityRecommendationId { get; set; }
		public int UserId { get; set; }
		public string WorkoutRecommendations { get; set; }
		public string ActivityGoals { get; set; }
		public string CustomActivityAdvice { get; set; }

		// Navigation property to the User
		public virtual UserModel User { get; set; }
	}
}
