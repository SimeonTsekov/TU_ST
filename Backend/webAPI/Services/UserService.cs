using webAPI.Authentication.JwtBearer;
using webAPI.Interfaces;
using webAPI.DTOs.Request;
using webAPI.DTOs.Response;
using webApi.Data.Models;

namespace webAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;

        public UserService(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public JwtResponse Login(UserLoginRequest loginRequest)
        {
            if(loginRequest.Email == null || loginRequest.Password == null)
            {
                throw new InvalidOperationException(); // Removes the green lines that say "property may be null"
            }

            var user = _userRepository.FindUserByEmail(loginRequest.Email);

            if (user == null || !VerifyPassword(user, loginRequest.Password))
            {
                return null;
            }

            // Authentication successful, create a session
            return CreateSession(user);
        }

        public JwtResponse Register(UserRegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }

        private JwtResponse CreateSession(UserModel user)
        {
            var jwt = _jwtProvider.Generate(user);

            return new JwtResponse()
            {
                AccessToken = jwt,
                User = user
            };
        }

        private bool VerifyPassword(UserModel user, string password)
        {
            return false;
        }
    }
}
