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
            //TODO: 1) Add roles to the UserModel 

            //TODO: 2) Seed admin and user roles in the DB

            //TODO: 3) Create default admin user

            //TODO: 4) In JWT provider, add claims for roler

            //TODO: 5) Create attribute that checks the user's claims

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
