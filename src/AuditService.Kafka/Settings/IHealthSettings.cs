namespace AuditService.Kafka.Settings
{
    public interface IHealthSettings
    {
        int CriticalErrorsCount { get; set; }

        int ForPeriodInSec { get; set; }
    }
}
