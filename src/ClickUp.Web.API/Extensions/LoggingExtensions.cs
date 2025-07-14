using Serilog;
using Serilog.Events;

namespace ClickUp.Web.API.Extensions;

public static class LoggingExtensions
{
    public static void ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "ClickUp.Web.API")
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown")
            .CreateLogger();

        services.AddSerilog();
    }

    public static void AddStructuredLogging(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ILogger>(provider => 
            provider.GetRequiredService<ILogger<object>>());
    }
}