using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
	public class ActivityRecommendationModel
	{
		[Key]
		public int ActivityRecommendationId { get; set; }

		public string WorkoutRecommendations { get; set; } = null!;

		public string ActivityGoals { get; set; } = null!;

		public string CustomActivityAdvice { get; set; } = null!;

		[ForeignKey(nameof(UserModel))]
		public int UserId { get; set; }

		[Required] 
		public virtual UserModel UserModel { get; set; } = null!;
	}
}
