using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class SwaggerSettings : ISwaggerSettings
{
    public SwaggerSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string[]? XmlComments { get; set; }

    /// <summary>
    ///     Apply settings
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        XmlComments = configuration.GetSection("SWAGGER:XmlComments").Get<string[]>();
    }
}