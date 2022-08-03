namespace KIT.Kafka.Settings.Interfaces;

/// <summary>
///     Configuration section of kafka topics
/// </summary>
public interface IKafkaTopics
{
    /// <summary>
    ///     Topic of PlayerChangesLog
    /// </summary>
    string PlayerChangesLog { get; set; }

    /// <summary>
    ///     Topic of BlockedPlayersLog
    /// </summary>
    string BlockedPlayersLog { get; set; }

    /// <summary>
    ///     Topic of AuditLog
    /// </summary>
    string AuditLog { get; set; }

    /// <summary>
    ///     Topic of Permissions
    /// </summary>
    string Permissions { get; set; }

    /// <summary>
    ///     Topic of HealthCheck
    /// </summary>
    string HealthCheck { get; set; }

    /// <summary>
    ///     Topic of SsoPlayersChangesLog
    /// </summary>
    string SsoPlayersChangesLog { get; set; }

    /// <summary>
    ///     Topic of SsoUsersChangesLog
    /// </summary>
    string SsoUsersChangesLog { get; set; }

    /// <summary>
    ///     Topic of Visitlog
    /// </summary>
    string Visitlog { get; set; }

    /// <summary>
    ///     Topic of localization changes
    /// </summary>
    string LocalizationChanged { get; set; }
}