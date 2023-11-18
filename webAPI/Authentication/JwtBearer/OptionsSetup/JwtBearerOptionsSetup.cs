using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace webAPI.Authentication.JwtBearer.OptionsSetup
{
    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtBearerSettings _jwtBearerSettings;

        public JwtBearerOptionsSetup(IOptions<JwtBearerSettings> settings)
        {
            _jwtBearerSettings = settings.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _jwtBearerSettings.Issuer,
                ValidAudience = _jwtBearerSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtBearerSettings.SigningKey)),
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
