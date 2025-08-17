using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManagement.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        //    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        //    {
        //        services.AddAuthentication(options =>
        //        {
        //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(options =>
        //        {
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ValidateIssuerSigningKey = true,
        //                ValidIssuer = "Issuer", 
        //                ValidAudience = "Audience",
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey_AtLeast32CharsLongMustBe")),
        //                ClockSkew = TimeSpan.Zero
        //            };
        //        });
        //        // ADD THIS PART BELOW 🔥
        //        options.Events = new JwtBearerEvents
        //        {
        //            OnAuthenticationFailed = context =>
        //            {
        //                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
        //                return Task.CompletedTask;
        //            },
        //            OnTokenValidated = context =>
        //            {
        //                Console.WriteLine("Token validated successfully!");
        //                return Task.CompletedTask;
        //            }
        //        };
        //        return services;
        //    }
        //}
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => // 👈 inside this options =>
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

                // 🔥 Inside this block!
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully!");
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
