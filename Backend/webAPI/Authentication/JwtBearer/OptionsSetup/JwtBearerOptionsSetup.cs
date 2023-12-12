using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace webAPI.Authentication.JwtBearer.OptionsSetup
{
    public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtBearerSettings _jwtBearerSettings;

        public JwtBearerOptionsSetup(IOptions<JwtBearerSettings> settings)
        {
            this._jwtBearerSettings = settings.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = this._jwtBearerSettings.Issuer,
                ValidAudience = this._jwtBearerSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtBearerSettings.SigningKey
                    ?? throw new InvalidOperationException("Missing JWT signing key!"))),
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
