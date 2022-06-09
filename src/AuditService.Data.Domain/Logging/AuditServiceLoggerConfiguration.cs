using AuditService.Data.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace AuditService.Data.Domain.Logging;

public class AuditServiceLoggerConfiguration
{
    public string Timestamp { get; set; } 

    public LogLevel Level { get; set; }

    public LogChannel Channel { get; set; }

    public string Message  { get; set; }

    public object? Context { get; set; }
}
