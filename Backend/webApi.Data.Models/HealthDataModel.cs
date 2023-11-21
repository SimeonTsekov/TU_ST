using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
	public class HealthDataModel
	{
		[Key]
		public int HealthDataId { get; set; }

		public float BodyMass { get; set; }

		public float BMI { get; set; }

		public float BodyFat { get; set; }

		public float LeanBodyMass { get; set; }

		public string SleepAnalysis { get; set; } = null!;

		[ForeignKey(nameof(UserModel))]
		public int UserId { get; set; }

		[Required]
		public virtual UserModel UserModel { get; set; } = null!;
	}
}
