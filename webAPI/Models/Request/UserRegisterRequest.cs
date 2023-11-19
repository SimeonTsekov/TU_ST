namespace webAPI.Models.Request
{
    public class UserRegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
    }
}
