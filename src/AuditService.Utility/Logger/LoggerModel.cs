using Microsoft.Extensions.Logging;

namespace AuditService.Utility.Logger
{
    /// <summary>
    ///     Model of custom logger
    /// </summary>
    public class LoggerModel
    {
        /// <summary>
        ///     Timestamp
        /// </summary>
        public string? Timestamp { get; set; }

        /// <summary>
        ///     Level for logging
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        ///     Channel
        /// </summary>
        public string? Channel { get; set; }

        /// <summary>
        ///     Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        ///     Context
        /// </summary>
        public object? Context { get; set; }
    }
}