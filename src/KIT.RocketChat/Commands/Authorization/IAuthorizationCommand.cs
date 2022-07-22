using KIT.RocketChat.ApiClient.Methods.BaseEntities;

namespace KIT.RocketChat.Commands.Authorization;

/// <summary>
///     RocketChat command to perform authorization
/// </summary>
public interface IAuthorizationCommand
{
    /// <summary>
    ///     Execute a command
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>RocketChat authentication data</returns>
    Task<AuthData> Execute(CancellationToken cancellationToken);
}