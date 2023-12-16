using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class ActivityDataModel : BaseModel
{
    public int Workouts { get; set; }

    public int DailySteps { get; set; }

    public float DailyDistance { get; set; }

    public float DailyEnergyBurned { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ActivityDataModels")]
    public virtual UserModel? User { get; set; }
}