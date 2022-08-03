namespace AuditService.Common.Models.Dto;

/// <summary>
///     Export data model
/// </summary>
public class ExportDataResponseDto
{
    public ExportDataResponseDto(string fileName, byte[] content, string contentType)
    {
        FileName = fileName;
        Content = content;
        ContentType = contentType;
    }

    /// <summary>
    ///     File name
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    ///     Byte array
    /// </summary>
    public byte[] Content { get; set; }

    /// <summary>
    ///     Content type
    /// </summary>
    public string ContentType { get; set; }
}