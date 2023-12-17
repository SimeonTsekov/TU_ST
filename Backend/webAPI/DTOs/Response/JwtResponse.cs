namespace webAPI.DTOs.Response
{
    public class JwtResponse
    {
        public string? AccessToken { get; set; }
        public UserResponse? User { get; set; }
    }
}
