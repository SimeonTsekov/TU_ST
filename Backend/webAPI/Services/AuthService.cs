using System.Diagnostics;
using AutoMapper;
using webAPI.Authentication.JwtBearer;
using webAPI.Interfaces;
using webAPI.DTOs.Request;
using webAPI.DTOs.Response;
using webApi.Data.Models;
using webAPI.DTOs;

namespace webAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IJwtProvider jwtProvider, IUserRepository userRepository, IMapper mapper)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public JwtResponse Login(UserLoginRequest loginRequest)
        {
            var user = _userRepository.FindUserByEmail(loginRequest.Email!);

            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials!");
            }

            return CreateSession(user);
        }

        public JwtResponse Register(UserRegisterRequest registerRequest)
        {
            try
            {
                _userRepository.FindUserByEmail(registerRequest.Email!);

                // If the above line doesn't throw an exception, it means that the user exists, so the email is already taken
                // and we can't register the user, so we throw an exception to be caught in the controller
                throw new InvalidOperationException("User with this email already exists!");
            }
            catch (System.NullReferenceException)
            {
                // ignored
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            var newUser = new UserModel
            {
                Email = registerRequest.Email!,
                Password = hashedPassword,
                Username = registerRequest.Username!,
                Age = registerRequest.Age,
                Height = registerRequest.Height,
            };

            _userRepository.Create(newUser);

            return CreateSession(newUser);
        }

        private JwtResponse CreateSession(UserModel user)
        {
            var jwt = _jwtProvider.Generate(user);

            return new JwtResponse()
            {
                AccessToken = jwt,
                User = _mapper.Map<UserResponse>(user)
            };
        }
    }
}
