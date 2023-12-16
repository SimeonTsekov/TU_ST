using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace webApi.Data.Models;

public partial class UserModel : BaseModel
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int Age { get; set; }

    public int Height { get; set; }

    [Column(TypeName = "nvarchar(20)")] 
    [EnumDataType(typeof(SexEnum))]    
    public SexEnum? Sex { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ActivityDataModel> ActivityDataModels { get; set; } = new List<ActivityDataModel>();

    [InverseProperty("User")]
    public virtual ICollection<ActivityRecommendationModel> ActivityRecommendationModels { get; set; } = new List<ActivityRecommendationModel>();

    [InverseProperty("User")]
    public virtual ICollection<HealthDataModel> HealthDataModels { get; set; } = new List<HealthDataModel>();

    [InverseProperty("User")]
    public virtual ICollection<HealthRecommendationModel> HealthRecommendationModels { get; set; } = new List<HealthRecommendationModel>();

    [InverseProperty("User")]
    public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
}