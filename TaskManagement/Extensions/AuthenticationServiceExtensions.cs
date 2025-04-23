using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManagement.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Issuer", 
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey_AtLeast32CharsLongMustBe")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
    