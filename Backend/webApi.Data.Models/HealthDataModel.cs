using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class HealthDataModel
{
    [Key]
    public int HealthDataId { get; set; }

    [Required]
    public float BodyMass { get; set; }

    [Required]
    [Column("BMI")]
    public float Bmi { get; set; }

    [Required]
    public float BodyFat { get; set; }

    [Required]
    public float LeanBodyMass { get; set; }

    [Required]
    public string SleepAnalysis { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HealthDataModels")]
    public virtual UserModel User { get; set; }
}