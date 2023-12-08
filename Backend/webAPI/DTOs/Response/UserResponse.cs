using webAPI.DTOs.Response;

namespace webAPI.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        
        public int? Age { get; set; }

        public int? Height { get; set; }
        
        public virtual ICollection<ActivityResponse>? ActivityData { get; set; }
        
        public virtual ICollection<HealthDataResponse>? HealthData { get; set; }
    }
}
