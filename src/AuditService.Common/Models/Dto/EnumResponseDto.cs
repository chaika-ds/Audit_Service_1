namespace AuditService.Common.Models.Dto;

public class EnumResponseDto
{
    public EnumResponseDto() { }
    public EnumResponseDto(string value, string? description)
    {
        Value = value;
        Description = description;
    }

    public string? Description { get; set; }
    public string Value { get; set; }
}