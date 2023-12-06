using webAPI.DTOs.Response;

namespace webAPI.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
        // Note: Storing passwords as plain text is not secure
        public int? Age { get; set; }

        public int? Height { get; set; }
        // Navigation property for related ActivityData records
        public virtual ICollection<ActivityResponse>? ActivityData { get; set; }
        // Navigation property for related HealthData records
        public virtual ICollection<HealthDataDTO>? HealthData { get; set; }
    }
}
