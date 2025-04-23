using Serilog;

namespace TaskManagement.Utilities
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
