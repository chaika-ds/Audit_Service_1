namespace AuditService.Kafka.Services.Health;

public interface IHealthService
{
    long GetErrorsCount();
}
