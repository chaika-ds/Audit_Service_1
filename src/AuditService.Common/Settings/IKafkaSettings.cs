namespace AuditService.Kafka.Settings
{
    public interface IKafkaSettings
    {
        string GroupId { get; }

        string Address { get; }

        string Topic { get; }

        Dictionary<string, string> Config { get; }
    }
}
