using Tolar.MinioService.Client;

namespace AuditService.Tests.Fakes.Setup.Minio
{
    /// <summary>
    ///     Fake minio settings
    /// </summary>
    public class MinioSettingsFake : IFileStorageSettings
    {
        /// <summary>
        ///     Fake string value
        /// </summary>
        private const string fakeStringValue = "test";

        public MinioSettingsFake()
        {
            Endpoint = fakeStringValue;
            AccessKey = fakeStringValue;
            SecretKey = fakeStringValue;
        }

        /// <summary>
        ///     Endpoint to connect to minio fake
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        ///     Access key(Credential to connect) fake
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        ///     Secret key(Credential to connect) fake
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        ///     Connects to Cloud Storage with HTTPS fake
        /// </summary>
        public bool WithSSL { get; set; }

        /// <summary>
        ///     HTTP tracing On.Writes output to Console fake
        /// </summary>
        public bool TraceRequests { get; set; }
    }
}
