using Microsoft.Extensions.Configuration;

namespace AuditService.Kafka.AppSetings;

public class HealthSettings : IHealthSettings
{
    public HealthSettings(IConfiguration configuration) => ApplySettings(configuration);

    public int CriticalErrorsCount { get; set; }

    public int ForPeriodInSec { get; set; }

    /// <summary>
    ///     Apply Health configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        CriticalErrorsCount = int.Parse(config["Kafka:HealthCheck:CriticalErrorsCount"]);
        ForPeriodInSec = int.Parse(config["Kafka:HealthCheck:ForPeriodInSecond"]);
    }
}