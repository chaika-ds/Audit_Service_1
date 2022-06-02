namespace AuditService.EventProducer;

public interface IDirectorSettings
{
    public Dictionary<string, string> Topics { get; }
}

