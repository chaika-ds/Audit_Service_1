namespace AuditService.WebApiApp.Services.Interfaces;

public interface IHealthCheck
{
    bool CheckElkHealth();
    bool CheckKafkaHealth();
}