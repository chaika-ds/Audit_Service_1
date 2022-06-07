using AuditService.Data.Domain.Logging;
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
    }
}
