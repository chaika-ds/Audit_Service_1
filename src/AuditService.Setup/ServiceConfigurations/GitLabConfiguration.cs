using AuditService.Setup.AppSettings;
using GitLabApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Configuration of GitLab
/// </summary>
public static class GitLabConfiguration
{
    /// <summary>
    ///     Create scope for GitLabClient
    /// </summary>
    public static void AddGitLabClient(this IServiceCollection services)
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