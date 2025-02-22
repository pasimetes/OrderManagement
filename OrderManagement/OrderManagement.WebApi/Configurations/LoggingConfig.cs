using Serilog;

namespace OrderManagement.WebApi.Configurations
{
    public static class LoggingConfig
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} (CorrelationId: {correlationId}) [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
