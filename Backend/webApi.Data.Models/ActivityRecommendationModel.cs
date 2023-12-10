using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models;

public partial class ActivityRecommendationModel : BaseModel
{
    [Required]
    [Column(TypeName = "text")]
    public string? Recommendation { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ActivityRecommendationModels")]
    public virtual UserModel? User { get; set; }
}