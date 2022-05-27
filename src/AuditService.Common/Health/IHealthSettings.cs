namespace AuditService.Common.Health
{
    public interface IHealthSettings
    {
        int CriticalErrorsCount { get; set; }

        int ForPeriodInSec { get; set; }
    }
}
