namespace AuditService.WebApiApp.Services.Interfaces;

public interface IPermissionPusherSettings
{
    public string Topic { get; }

    public Guid ServiceId { get; set; }

    public string ServiceName { get; set; }
}

