using System.Text.Json.Serialization;

namespace AuditService.Data.Domain.Enums;

/// <summary>
///     NODE TYPE
/// </summary>
// ReSharper disable InconsistentNaming
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NodeType
{
    ROOT = 0,

    PROJECT = 1,

    FOLDER = 2,

    HALL = 3
}