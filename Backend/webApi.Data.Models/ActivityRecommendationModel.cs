using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class ActivityRecommendationModel
{
    [Key]
    public int ActivityRecommendationId { get; set; }

    [Required]
    public string Recommendation { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ActivityRecommendationModels")]
    public virtual UserModel User { get; set; }
}