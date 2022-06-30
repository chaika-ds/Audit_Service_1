namespace AuditService.Kafka.AppSettings
{
    public interface IHealthSettings
    {
        int CriticalErrorsCount { get; set; }

        int ForPeriodInSec { get; set; }
    }
}
