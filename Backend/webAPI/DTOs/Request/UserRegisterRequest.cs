using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webApi.Data.Models;

namespace webAPI.DTOs.Request
{
    public class UserRegisterRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match.")]
        public string? Password { get; set; }

        [NotMapped]
        public string? ConfirmPassword { get; set; }
    }
}
