using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class JsonDataSettings : IJsonDataSettings
{
    public JsonDataSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? ServiceCategories { get; set; }

    /// <summary>
    ///     Apply JSON Data configs
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        ServiceCategories = configuration["JsonData:ServiceCategories"];
    }
}