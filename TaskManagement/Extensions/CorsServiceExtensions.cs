namespace TaskManagement.Extensions
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}


