using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class ActivityDataModel
{
    [Key]
    public int ActivityDataId { get; set; }

    [Required]
    public int Workouts { get; set; }

    [Required]
    public int DailySteps { get; set; }

    [Required]
    public float DailyDistance { get; set; }

    [Required]
    public float DailyEnergyBurned { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ActivityDataModels")]
    public virtual UserModel User { get; set; }
}