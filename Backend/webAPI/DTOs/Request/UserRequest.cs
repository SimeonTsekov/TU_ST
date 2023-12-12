namespace webAPI.DTOs.Request;

public class UserRequest
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    
    public int? Age { get; set; }

    public int? Height { get; set; }
}