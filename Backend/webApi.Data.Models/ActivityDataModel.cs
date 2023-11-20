using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
	public class ActivityDataModel
	{
		[Key]
		public int ActivityDataId { get; set; }

		public int Workouts { get; set; }

		public int DailySteps { get; set; }

		public float DailyDistance { get; set; }

		public float DailyEnergyBurned { get; set; }

		[ForeignKey(nameof(UserModel))]
		public int UserId { get; set; }

		[Required]
		public virtual UserModel UserModel { get; set; } = null!;
	}
}
