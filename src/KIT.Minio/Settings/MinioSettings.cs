using Microsoft.Extensions.Configuration;
using Tolar.MinioService.Client;

namespace KIT.Minio.Settings;

/// <summary>
///     Minio settings 
/// </summary>
internal class MinioSettings : IFileStorageSettings
{
    public MinioSettings(IConfiguration configuration)
    {
        Endpoint = configuration["Minio:Endpoint"];
        AccessKey = configuration["Minio:AccessKey"];
        SecretKey = configuration["Minio:SecretKey"];
        WithSSL = bool.Parse(configuration["Minio:WithSSL"]);
        TraceRequests = bool.Parse(configuration["Minio:TraceRequests"]);
    }

    /// <summary>
    ///     Endpoint to connect to minio
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    ///     Access key(Credential to connect)
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    ///     Secret key(Credential to connect)
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    ///     Connects to Cloud Storage with HTTPS
    /// </summary>
    public bool WithSSL { get; set; }

    /// <summary>
    ///     HTTP tracing On.Writes output to Console
    /// </summary>
    public bool TraceRequests { get; set; }
}