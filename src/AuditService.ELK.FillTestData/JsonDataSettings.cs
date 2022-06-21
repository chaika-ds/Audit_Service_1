using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData
{
    public class JsonDataSettings : IJsonDataSettings
    {
        /// <summary>
        ///     JSON data settings
        /// </summary>
        public JsonDataSettings(IConfiguration config)
        {
            ApplyJsonDataSection(config); 
        }

        #region JSON Data

        /// <summary>
        ///     Categories of service
        /// </summary>
        public string? ServiceCategories { get; set; }

        /// <summary>
        ///     Apply JSON Data configs
        /// </summary>
        private void ApplyJsonDataSection(IConfiguration configuration)
        {
            ServiceCategories = configuration["JSON_DATA:SERVICE_CATEGORIES_PATH"];
        }

        #endregion
    }
}
