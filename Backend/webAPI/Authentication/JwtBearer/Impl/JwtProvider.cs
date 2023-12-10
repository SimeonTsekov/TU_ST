using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webApi.Data.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace webAPI.Authentication.JwtBearer.impl
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtBearerSettings _jwtBearerSettings;

        public JwtProvider(IOptions<JwtBearerSettings> jwtBearerSettingsOptions)
        {
            _jwtBearerSettings = jwtBearerSettingsOptions.Value;
        }

        public string Generate(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var expiry = now.Add(TimeSpan.FromHours(_jwtBearerSettings.LifeSpan));
            var key = Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey ?? throw new InvalidOperationException());

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            };

            var signingCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            );

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                Issuer = _jwtBearerSettings.Issuer,
                Audience = _jwtBearerSettings.Audience,
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
