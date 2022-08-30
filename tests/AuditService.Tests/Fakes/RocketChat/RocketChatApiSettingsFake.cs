using KIT.RocketChat.Settings.Interfaces;

namespace AuditService.Tests.Fakes.RocketChat
{
    /// <summary>
    ///  RocketChat api settings fake
    /// </summary>
    internal class RocketChatApiSettingsFake : IRocketChatApiSettings
    {
        /// <summary>
        ///     Fake text value
        /// </summary>
        private const string textValueFake = "test";

        /// <summary>
        ///     Fake url
        /// </summary>
        private const string urlFake = "https://test.test";

        public RocketChatApiSettingsFake(bool isActive)
        {
            IsActive = isActive;
            User = textValueFake;
            Password = textValueFake;
            BaseApiUrl = urlFake;
            ApiVersion = textValueFake;
        }

        /// <summary>
        ///     Flag indicating chat activity fake
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///     API user to authenticate fake
        /// </summary>
        public string? User { get; set; }

        /// <summary>
        ///     API password to authenticate fake
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        ///     Base url for api fake
        /// </summary>
        public string? BaseApiUrl { get; set; }

        /// <summary>
        ///     Api version fake
        /// </summary>
        public string? ApiVersion { get; set; }
    }
}
