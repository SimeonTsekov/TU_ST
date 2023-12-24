using System.ComponentModel.DataAnnotations;
using webApi.Data.Models;
using webAPI.Utils;

namespace webAPI.DTOs.Request;

public class UserRequest
{
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
    public string? Password { get; set; }

    [CustomRangeValidation(18, 99, "Age value should be between 18 and 99.")]
    public int? Age { get; set; } = -1;

    [CustomRangeValidation(100, 250, "Height value should be between 100 and 250.")]
    public int? Height { get; set; } = -1;

    [CustomEnumValidation(typeof(Sex), "Invalid sex value.")]
    public string? Sex { get; set; } = "None";
}