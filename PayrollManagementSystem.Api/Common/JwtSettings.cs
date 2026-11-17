using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PayrollManagementSystem.ViewModels.Common;
using System.Text;

namespace PayrollManagementSystem.Api.Common
{
    public class JwtSettings
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var configration = builder.Configuration;
            var jwtSection = configration.GetSection("Jwt");
            builder.Services.Configure<JwtConfigVm>(jwtSection);

            JwtConfigVm jwtConfig = jwtSection.Get<JwtConfigVm>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Key);

            builder.Services.AddAuthentication(e =>
            {
                e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(e =>
            {
                e.RequireHttpsMetadata = true;
                e.SaveToken = true;
                e.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }
    }
}
