using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Data.Models
{
    public class Role : SmartEnum<Role>
    {
        public static readonly Role AdminRole = new("Admin", 1);
        public static readonly Role UserRole = new("User", 2);

        public Role(string name, int value) : base(name, value)
        {
        }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
