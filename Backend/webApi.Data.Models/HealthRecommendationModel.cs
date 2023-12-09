using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class HealthRecommendationModel
{
    [Key]
    public int HealthRecommendationId { get; set; }

    [Required]
    public string Recommendation { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("HealthRecommendationModels")]
    public virtual UserModel User { get; set; }
}