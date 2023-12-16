using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class HealthDataModel : BaseModel
{
    public float BodyMass { get; set; }

    [Column("BMI")]
    public float Bmi { get; set; }

    public float BodyFat { get; set; }

    public float LeanBodyMass { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HealthDataModels")]
    public virtual UserModel? User { get; set; }
}