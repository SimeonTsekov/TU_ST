using Microsoft.Extensions.Options;

namespace webAPI.Authentication.JwtBearer.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtBearerSettings>
    {
        private const string SectionName = "JwtBearer";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void Configure(JwtBearerSettings options)
        {
            this._configuration.GetSection(SectionName).Bind(options);
        }
    }
}
