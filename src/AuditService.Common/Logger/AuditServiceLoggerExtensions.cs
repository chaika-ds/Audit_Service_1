using AuditService.Data.Domain.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace AuditService.Common.Logger
{
    /// <summary>
    /// Etension methods on ILoggingBuilder that used to register the Audit service logger custom provider
    /// </summary>
    public static class AuditServiceLoggerExtensions
    {
        public static ILoggingBuilder AddAuditServiceLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, AuditServiceLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions<AuditServiceLoggerConfiguration, AuditServiceLoggerProvider>(builder.Services);
            return builder;
        }

        public static ILoggingBuilder AddAuditServiceLogger(this ILoggingBuilder builder,
            Action<AuditServiceLoggerConfiguration> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            builder.AddAuditServiceLogger();
            builder.Services.Configure(configure);

            return builder;
        }

        //private static void AddAuditServiceLogger(this Action<ILoggingBuilder> builder, IServiceCollection services, IConfiguration configuration)
        //{
       
        //    builder.AddConfiguration();

        //    services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, AuditServiceLoggerProvider>());

        //    builder(new LoggingBuilder(services));
        //    services.AddLogging(configure => configure..AddSerilog())
        //            .AddTransient<MyClass>();

        //    if (configuration["LOG_LEVEL"] == "true")
        //    {
        //        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);
        //    }
        //    else
        //    {
        //        services.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
        //    }
        //}
    }
}
