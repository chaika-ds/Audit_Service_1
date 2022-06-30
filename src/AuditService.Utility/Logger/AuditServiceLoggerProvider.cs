using System.Collections.Concurrent;
using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuditService.Utility.Logger;

/// <summary>
///     Provider for custom Audit service logger instance
/// </summary>
[UnsupportedOSPlatform("browser")]
[ProviderAlias("AuditServiceConsole")]
public class AuditServiceLoggerProvider : ILoggerProvider, ISupportExternalScope
{
    private readonly ConcurrentDictionary<string, AuditServiceConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    private LoggerModel _currentConfig;
    private IDisposable? _onChangeToken;

    public AuditServiceLoggerProvider(IOptionsMonitor<LoggerModel> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    }

    internal IExternalScopeProvider ScopeProvider { get; private set; }

    public ILogger CreateLogger(string categoryName) 
        => _loggers.GetOrAdd(categoryName, _ => new AuditServiceConsoleLogger(categoryName, GetCurrentConfig, this, null));

    void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider) 
        => ScopeProvider = scopeProvider;

    private LoggerModel GetCurrentConfig() 
        => _currentConfig;

    public bool IsEnabled(LogLevel logLevel) 
        => logLevel != LogLevel.None && _currentConfig.Level != LogLevel.None && Convert.ToInt32(logLevel) >= Convert.ToInt32(_currentConfig.Level);

    public void Dispose()
    {
        if (_onChangeToken == null)
            return;

        _loggers.Clear();
        _onChangeToken.Dispose();
        _onChangeToken = null;
    }
}