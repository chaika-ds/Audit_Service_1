using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace AuditService.Data.Domain.Logging
{
    /// <summary>
    /// Provider for custom Audit service logger instance
    /// </summary>
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("AuditServiceConsole")]
    public class AuditServiceLoggerProvider : ILoggerProvider, IDisposable,  ISupportExternalScope
    {
        IExternalScopeProvider _scopeProvider;
        private readonly ConcurrentDictionary<string, AuditServiceConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        private AuditServiceLoggerConfiguration _currentConfig;
        public bool IsDisposed { get; protected set; }
        private IDisposable? _onChangeToken;

        internal IExternalScopeProvider ScopeProvider
        {
            get
            {
                if (_scopeProvider == null)
                    _scopeProvider = new LoggerExternalScopeProvider();
                return _scopeProvider;
            }
        }

        public AuditServiceLoggerProvider(IOptionsMonitor<AuditServiceLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        void ISupportExternalScope.SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, category => {
                return new AuditServiceConsoleLogger(categoryName, GetCurrentConfig, this, null);});        

        private AuditServiceLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public bool IsEnabled(LogLevel logLevel)
        {
            bool Result = logLevel != LogLevel.None
                && _currentConfig.Level != LogLevel.None
                && Convert.ToInt32(logLevel) >= Convert.ToInt32(_currentConfig.Level);

            return Result;
        }

        public void Dispose()
        {
            if (_onChangeToken != null)
            {
                _loggers.Clear();
                _onChangeToken.Dispose();
                _onChangeToken = null;
            }
        }        
    }
}
