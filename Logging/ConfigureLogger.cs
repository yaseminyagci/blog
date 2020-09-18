using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RollingFileAlternate;

namespace Logging
{
    public static class ConfigureLogger
    {
        public static ILogger BuildLoggerConfiguration(this LoggerConfiguration loggerConfiguration)
        {
            return loggerConfiguration.MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Migrations", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .Enrich.WithUserName()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341") // <- Added
                .WriteTo.RollingFileAlternate(new RenderedCompactJsonFormatter(), "../Logging/Logs", fileSizeLimitBytes: 314572800) //300mb
                .CreateLogger();
        }
    }
}
