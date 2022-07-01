using Microsoft.Extensions.Logging;

namespace AuditService.Utility.Logger;

/// <summary>
///     Custom logger for console in Audit log service
/// </summary>
public class AuditServiceConsoleLogger : ILogger
{
    private AuditServiceLoggerProvider Provider { get; }
    private readonly Func<LoggerModel> _getCurrentConfig;
    private readonly string? _logPrefix;
    private readonly string _categoryName;

    public AuditServiceConsoleLogger(string categoryName, Func<LoggerModel> getCurrentConfig, AuditServiceLoggerProvider provider, string? logPrefix)
    {
        (_categoryName, _getCurrentConfig, Provider, _logPrefix) = (categoryName, getCurrentConfig, provider, logPrefix);
    }

    public IDisposable BeginScope<TState>(TState state) => Provider.ScopeProvider.Push(state);

    public bool IsEnabled(LogLevel logLevel) => Provider.IsEnabled(logLevel);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) 
            return;

        AuditServiceConsoleLoggerExtension.WriteToLog(logLevel, _getCurrentConfig().Channel, _logPrefix + formatter(state, exception), _categoryName);
    }
}