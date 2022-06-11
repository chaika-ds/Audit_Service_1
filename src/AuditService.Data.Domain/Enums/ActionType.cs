using System.Text.Json.Serialization;

namespace AuditService.Data.Domain.Enums;

/// <summary>
///     Type of action
/// </summary>
// ReSharper disable InconsistentNaming
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActionType
{
    CREATE = 0,

    EDIT = 1,

    DELETE = 2,

    SOFT_DELETE = 3,

    EXPORT = 4,

    CONFIRM = 5,

    DENY = 6,

    BIND = 7,

    UNBIND = 8,

    SYNCHRONIZATION = 9,

    VERIFICATION = 10,

    LOGIN = 11,

    LOGOUT = 12,
    
    VIEW = 13
}