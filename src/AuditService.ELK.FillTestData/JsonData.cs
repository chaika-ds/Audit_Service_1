using AuditService.Setup.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData
{
    public class JsonData : IJsonData
    {
        /// <summary>
        ///     JSON data settings
        /// </summary>
        public JsonData(IConfiguration config)
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
            ServiceCategories = configuration["JsonData:ServiceCategories"];
        }

        #endregion
    }
}
