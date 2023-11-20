using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
	public class HealthRecommendationModel
	{
		[Key]
		public int HealthRecommendationId { get; set; }

		public string DietaryAdvice { get; set; } = null!;

		public string SleepAdvice { get; set; } = null!;

		public string GeneralHealthAdvice { get; set; } = null!;

		[ForeignKey(nameof(UserModel))]
		public int UserId { get; set; }

		[Required] 
		public virtual UserModel UserModel { get; set; } = null!;
	}
}
