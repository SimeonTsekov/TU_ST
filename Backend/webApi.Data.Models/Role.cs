using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
    public class Role : BaseModel
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Roles")]
        public virtual UserModel? User { get; set; }
        public RolesEnum? Roles { get; set; }
    }
}
