using AuditService.Utility.Helpers;
using Microsoft.Extensions.Logging;

namespace AuditService.Utility.Logger
{
    /// <summary>
    /// Custom logger for console in Audit log service
    /// </summary>
    public class AuditServiceConsoleLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _logPrefix;
        private readonly Func<AuditServiceLoggerConfiguration> _getCurrentConfig;
        public AuditServiceLoggerProvider _provider { get; private set; }

        public AuditServiceConsoleLogger(string categoryName, Func<AuditServiceLoggerConfiguration> getCurrentConfig,
            AuditServiceLoggerProvider provider, string logPrefix) =>
            (_categoryName, _getCurrentConfig, _provider, _logPrefix) = (categoryName, getCurrentConfig, provider, logPrefix);           

        public IDisposable BeginScope<TState>(TState state) => _provider.ScopeProvider.Push(state);

        public bool IsEnabled(LogLevel logLevel) =>_provider.IsEnabled(logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {

            if (!IsEnabled(logLevel))
            {
                return;
            }

            var config = _getCurrentConfig();            
            
            string message = _logPrefix;

            if (formatter != null)
            {
                message += formatter(state, exception);
            }

            var logMessage = new AuditServiceLoggerConfiguration()
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Level = logLevel,
                Channel = config.Channel,
                Message = message,
                Context = _categoryName,
            };

            var logMessageJson = JsonHelper.SerializeToString(logMessage);
            Console.WriteLine(logMessageJson);
        }
    }
}
