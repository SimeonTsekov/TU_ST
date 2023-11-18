using webAPI.Authentication.JwtBearer;
using webAPI.Models;
using webAPI.Models.Request;
using webAPI.Models.Response;

namespace webAPI.Services.impl
{
    public class UserService : IUserService
    {
        private readonly IJwtProvider _jwtProvider;

        public UserService(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public JwtResponse Login(UserLoginRequest loginRequest)
        {
            throw new NotImplementedException();
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
    }
}
