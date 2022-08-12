using AuditService.Setup.AppSettings;
using GitLabApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.ServiceConfigurations;

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
            
            var gitLubClient = new GitLabClient(configuration.Url);

            if (!string.IsNullOrEmpty(configuration.Username))
                gitLubClient.LoginAsync(configuration.Username, configuration.Password);

            return gitLubClient;
        });
    }
}