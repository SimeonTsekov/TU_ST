using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApi.Data.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public virtual UserModel? User { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
