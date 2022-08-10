namespace AuditService.Common.Models.Dto;

/// <summary>
///     Export file model
/// </summary>
public class ExportFileResponseDto
{
    public ExportFileResponseDto(string fileLink)
    {
        FileLink = fileLink;
    }

    /// <summary>
    ///     Link to file
    /// </summary>
    public string FileLink { get; set; }
}