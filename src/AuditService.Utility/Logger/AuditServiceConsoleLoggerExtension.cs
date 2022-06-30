using AuditService.Common.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuditService.Utility.Logger;

/// <summary>
///     Audit logger Exception
/// </summary>
public static class AuditServiceConsoleLoggerExtension
{
    /// <summary>
    ///     Write to log exception
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <param name="env">EnvironmentName</param>
    public static void WriteToLog(this Exception exception, string env)
    {
        WriteToLog(LogLevel.Error, env, exception.FullMessage());
    }

    /// <summary>
    ///     Write to log message
    /// </summary>
    /// <param name="level">Log level</param>
    /// <param name="channel">Channel</param>
    /// <param name="message">Message</param>
    /// <param name="context">Context</param>
    public static void WriteToLog(LogLevel level, string? channel, string message, object? context = null)
    {
        var logMessage = new LoggerModel
        {
            Timestamp = DateTime.UtcNow.ToString("o"),
            Level = level,
            Channel = channel,
            Message = message,
            Context = context
        };

        Console.WriteLine(JsonConvert.SerializeObject(logMessage, new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        }));
    }
}