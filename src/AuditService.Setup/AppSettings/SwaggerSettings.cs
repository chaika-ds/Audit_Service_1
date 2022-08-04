using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

/// <summary>
///     Configuration section of swagger
/// </summary>
internal class SwaggerSettings : ISwaggerSettings
{
    public SwaggerSettings(IConfiguration configuration)
    {
        XmlComments = configuration.GetSection("Swagger:XmlComments").Get<string[]>();
    }

    /// <summary>
    ///     XML comments for swagger
    /// </summary>
    public string[] XmlComments { get; set; }
}