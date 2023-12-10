using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class UserModel : BaseModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public int Height { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ActivityDataModel> ActivityDataModels { get; set; } = new List<ActivityDataModel>();

    [InverseProperty("User")]
    public virtual ICollection<ActivityRecommendationModel> ActivityRecommendationModels { get; set; } = new List<ActivityRecommendationModel>();

    [InverseProperty("User")]
    public virtual ICollection<HealthDataModel> HealthDataModels { get; set; } = new List<HealthDataModel>();

    [InverseProperty("User")]
    public virtual ICollection<HealthRecommendationModel> HealthRecommendationModels { get; set; } = new List<HealthRecommendationModel>();
}