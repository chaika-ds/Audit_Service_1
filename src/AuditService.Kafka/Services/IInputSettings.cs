namespace AuditService.Kafka.Services
{
    public interface IInputSettings<T>
    {
        string Name { get; set; }

        string Topic { get; set; }
    }
}
