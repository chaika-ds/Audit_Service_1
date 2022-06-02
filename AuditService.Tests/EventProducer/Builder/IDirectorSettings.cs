using System.Collections.Generic;

namespace AuditService.IntegrationTests;

public interface IDirectorSettings
{
    public Dictionary<string, string> Topics { get; }
}

