using System.Text.Json.Serialization;

namespace AuditService.Data.Domain.Enums
{
    /// <summary>
    ///     Sortable type
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortableType
    {
        Ascending = 1,
        Descending = 2
    }
}