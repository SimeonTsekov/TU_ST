using System.ComponentModel.DataAnnotations;

namespace webAPI.DTOs.Request
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        public string? Password { get; set; }
    }
}
