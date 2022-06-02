using System.Collections.Generic;

namespace AuditService.IntegrationTests.EventProducer.Builder;

public interface IDirectorSettings
{
    public Dictionary<string, string> Topics { get; }
}

