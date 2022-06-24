namespace AuditService.Common.Models.Dto;

/// <summary>
///     Response format for Enum
/// </summary>
public class EnumResponseDto
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EnumResponseDto" /> class.
    /// </summary>
    public EnumResponseDto(string value, string? description)
    {
        Value = value;
        Description = description;
    }

    /// <summary>
    ///     Enum Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    ///     Enum Value
    /// </summary>
    public string Value { get; set; }
}