namespace AuditService.WebApi.Models;

public class KafkaMessage
{
    public string ServiceName { get; set; }
    public Guid NodeId { get; set; }
    public string NodeType { get; set; }
    public string ActionName { get; set; }
    public string CategoryCode { get; set; }
    public string RequestUrl { get; set; }
    public string RequestBody { get; set; }
    public DateTime Timestamp { get; set; }
    public string EntityName { get; set; }
    public string EntityId { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public string UserIp { get; set; }
    public string UserLogin { get; set; }
    public string UserAgent { get; set; }
}