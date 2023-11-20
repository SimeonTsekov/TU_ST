namespace webAPI.Models.Response
{
    public class JwtResponse
    {
        public string AccessToken { get; set; }
        public UserModel User { get; set; }
    }
}
