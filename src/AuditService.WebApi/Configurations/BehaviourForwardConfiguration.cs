using System.IO.Compression;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace AuditService.WebApi.Configurations;

/// <summary>
///     Configure settings
/// </summary>
public static class BehaviourForwardConfiguration
{
    /// <summary>
    ///     Append new configurations to services
    /// </summary>
    public static void AdditionalConfigurations(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressModelStateInvalidFilter = true;
        });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.All;
            options.ForwardedForHeaderName = "X-Original-Forwarded-For";
            options.RequireHeaderSymmetry = false;
            options.ForwardLimit = null;
        });
    }
}