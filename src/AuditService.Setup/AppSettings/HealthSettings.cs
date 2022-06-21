using AuditService.Kafka.Settings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class HealthSettings : IHealthSettings
{
    public HealthSettings(IConfiguration configuration) => ApplySettings(configuration);

    public int CriticalErrorsCount { get; set; }

    public int ForPeriodInSec { get; set; }

    /// <summary>
    ///     Apply Health configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        CriticalErrorsCount = int.Parse(config["KAFKA:HEALTH_CHECK:CRITICAL_ERRORS_COUNT"]);
        ForPeriodInSec = int.Parse(config["KAFKA:HEALTH_CHECK:FOR_PERIOD_IN_SEC"]);
    }
}