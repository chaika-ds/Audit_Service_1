using AuditService.Common.Extensions;
using KIT.NLog.Consts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KIT.NLog.Extensions;

/// <summary>
///     Logger extensions.
///     Extends basic logging and makes it possible to specify a context model.
/// </summary>
public static class LoggerExtension
{
    /// <summary>
    ///     Log a message indicating the Trace level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogTrace(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Trace, message, contextModel);

    /// <summary>
    ///     Log a message indicating the Information level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogInformation(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Information, message, contextModel);

    /// <summary>
    ///     Log a message indicating the Debug level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogDebug(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Debug, message, contextModel);

    /// <summary>
    ///     Log a message indicating the Error level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogError(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Error, message, contextModel);

    /// <summary>
    ///     Log a message indicating the Critical level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogCritical(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Critical, message, contextModel);

    /// <summary>
    ///     Log a message indicating the Warning level.
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void LogWarning(this ILogger logger, string message, object contextModel) =>
        logger.Log(LogLevel.Warning, message, contextModel);

    /// <summary>
    ///     Log an exception
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="exception">Exception to log</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    /// <param name="logLevel">Log level</param>
    public static void LogException(this ILogger logger, Exception exception, string? message = null,
        object? contextModel = null,
        LogLevel logLevel = LogLevel.Error)
    {
        var fullMessage = $"{message} - Exception: {exception.FullMessage()}";

        if (contextModel is null)
        {
            logger.Log(logLevel, fullMessage);
            return;
        }

        logger.Log(logLevel, fullMessage, contextModel);
    }

    /// <summary>
    ///     Create a context for logging
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="contextModel">Context model</param>
    /// <param name="logAction">Action that will perform logging</param>
    public static void CreateContext(this ILogger logger, object contextModel, Action logAction)
    {
        var json = JsonConvert.SerializeObject(contextModel);

        using (logger.BeginScope(new[] { new KeyValuePair<string, object>(LogTemplateConst.ContextModel, json) }))
        {
            logAction();
        }
    }

    /// <summary>
    ///     Log a message
    /// </summary>
    /// <param name="logger">Logger instance</param>
    /// <param name="logLevel">Log level</param>
    /// <param name="message">Message to log</param>
    /// <param name="contextModel">Context model</param>
    public static void Log(this ILogger logger, LogLevel logLevel, string message, object contextModel) =>
        logger.CreateContext(contextModel, () => logger.Log(logLevel, message));
}