using KIT.RocketChat.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.RocketChat.Settings
{
    /// <summary>
    /// API RocketChat settings
    /// </summary>
    internal class RocketChatApiSettings : IRocketChatApiSettings
    {
        public RocketChatApiSettings(IConfiguration configuration)
        {
            ApplySettings(configuration);
        }

        /// <summary>
        ///     API user to authenticate
        /// </summary>
        public string? User { get; set; }

        /// <summary>
        ///     API password to authenticate
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        ///     Base url for api
        /// </summary>
        public string? BaseApiUrl { get; set; }

        /// <summary>
        ///     Api version
        /// </summary>
        public string? ApiVersion { get; set; }

        /// <summary>
        ///     Apply settings
        /// </summary>
        private void ApplySettings(IConfiguration configuration)
        {
            User = configuration["RocketChat:User"];
            Password = configuration["RocketChat:Password"];
            BaseApiUrl = configuration["RocketChat:BaseApiUrl"];
            ApiVersion = configuration["RocketChat:ApiVersion"];
        }
    }
}
