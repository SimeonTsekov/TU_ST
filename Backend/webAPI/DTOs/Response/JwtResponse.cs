using webApi.Data.Models;

namespace webAPI.DTOs.Response
{
    public class JwtResponse
    {
        public string? AccessToken { get; set; }
        public UserDTO? User { get; set; }
    }
}
