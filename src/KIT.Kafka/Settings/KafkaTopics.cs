using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.Kafka.Settings;

/// <summary>
///     Configuration section of kafka topics
/// </summary>
internal class KafkaTopics : IKafkaTopics
{
    public KafkaTopics(IConfiguration config)
    {
        AuditLog = config["Kafka:Topics:AuditLog"];
        Permissions = config["Kafka:Topics:PermissionsTopic"];
        HealthCheck = config["Kafka:Topics:HealthCheck"];
        BlockedPlayersLog = config["Kafka:Topics:BlockedPlayersLog"];
        PlayerChangesLog = config["Kafka:Topics:PlayerChangesLog"];
        SsoPlayersChangesLog = config["Kafka:Topics:SsoPlayersChangesLog"];
        SsoUsersChangesLog = config["Kafka:Topics:SsoUsersChangesLog"];
        Visitlog = config["Kafka:Topics:Visitlog"];
    }

    /// <summary>
    ///     Topic of PlayerChangesLog
    /// </summary>
    public string PlayerChangesLog { get; set; }

    /// <summary>
    ///     Topic of BlockedPlayersLog
    /// </summary>
    public string BlockedPlayersLog { get; set; }

    /// <summary>
    ///     Topic of AuditLog
    /// </summary>
    public string AuditLog { get; set; }

    /// <summary>
    ///     Topic of Permissions
    /// </summary>
    public string Permissions { get; set; }

    /// <summary>
    ///     Topic of HealthCheck
    /// </summary>
    public string HealthCheck { get; set; }

    /// <summary>
    ///     Topic of SsoPlayersChangesLog
    /// </summary>
    public string SsoPlayersChangesLog { get; set; }

    /// <summary>
    ///     Topic of SsoUsersChangesLog
    /// </summary>
    public string SsoUsersChangesLog { get; set; }

    /// <summary>
    ///     Topic of Visitlog
    /// </summary>
    public string Visitlog { get; set; }
}