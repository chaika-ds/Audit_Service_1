using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace AuditService.Utility.Logger
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

            LoggerProviderOptions.RegisterProviderOptions<LoggerModel, AuditServiceLoggerProvider>(builder.Services);
            return builder;
        }

        public static ILoggingBuilder AddAuditServiceLogger(this ILoggingBuilder builder,
            Action<LoggerModel> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            builder.AddAuditServiceLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
