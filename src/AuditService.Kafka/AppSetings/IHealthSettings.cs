namespace AuditService.Kafka.AppSetings
{
    public interface IHealthSettings
    {
        int CriticalErrorsCount { get; set; }

        int ForPeriodInSec { get; set; }
    }
}
