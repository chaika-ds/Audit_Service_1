using AuditService.Setup.AppSettings;
using GitLabApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Configuration of GitLub
/// </summary>
public static class GitLubConfiguration
{
    /// <summary>
    ///     Create scope for GitLabClient
    /// </summary>
    public static void AddGitLubClient(this IServiceCollection services)
    {
        services.AddScoped<IGitLabClient>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IGitlabSettings>();
            
            if (string.IsNullOrEmpty(configuration.Url))
                throw new ArgumentException($"{nameof(configuration.Url)} is null");
            
            return new GitLabClient(configuration.Url);
        });
    }
}