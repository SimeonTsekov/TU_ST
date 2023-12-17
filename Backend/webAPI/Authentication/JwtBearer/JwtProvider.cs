using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webApi.Data.Models;
using webAPI.Interfaces.Authentication;
using webAPI.Interfaces.User;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace webAPI.Authentication.JwtBearer
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtBearerSettings _jwtBearerSettings;
        private readonly IUserRepository _userRepository;

        public JwtProvider(IOptions<JwtBearerSettings> jwtBearerSettingsOptions, IUserRepository userRepository)
        {
            this._userRepository = userRepository;
            this._jwtBearerSettings = jwtBearerSettingsOptions.Value;
        }

        public string Generate(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var expiry = now.Add(TimeSpan.FromHours(this._jwtBearerSettings.LifeSpan));
            var key = Encoding.UTF8.GetBytes(this._jwtBearerSettings.SigningKey ?? throw new InvalidOperationException("Missing JWT signing key!"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            };

            var userRoles = this._userRepository.GetRolesForUser(user.Id);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var signingCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                Issuer = this._jwtBearerSettings.Issuer,
                Audience = this._jwtBearerSettings.Audience,
                SigningCredentials = signingCredentials,
                IssuedAt = now,
                NotBefore = now
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return jwt;
        }
    }
}
