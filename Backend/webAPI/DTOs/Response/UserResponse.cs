using webApi.Data.Models;

namespace webAPI.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Sex { get; set; }
    }
}
