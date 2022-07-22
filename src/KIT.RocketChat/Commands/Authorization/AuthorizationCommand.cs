using KIT.RocketChat.ApiClient;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.Login;
using KIT.RocketChat.ApiClient.Methods.Login.Enums;
using KIT.RocketChat.ApiClient.Methods.Login.Models;
using KIT.RocketChat.Settings.Interfaces;
using KIT.RocketChat.Storage;

namespace KIT.RocketChat.Commands.Authorization;

/// <summary>
///     RocketChat command to perform authorization
/// </summary>
internal class AuthorizationCommand : IAuthorizationCommand
{
    private readonly IRocketChatApiClient _apiClient;
    private readonly IRocketChatApiSettings _rocketChatApiSettings;
    private readonly IRocketChatStorage _rocketChatStorage;

    public AuthorizationCommand(IRocketChatApiClient apiClient, IRocketChatApiSettings rocketChatApiSettings,
        IRocketChatStorage rocketChatStorage)
    {
        _apiClient = apiClient;
        _rocketChatApiSettings = rocketChatApiSettings;
        _rocketChatStorage = rocketChatStorage;
    }

    /// <summary>
    ///     Execute a command
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>RocketChat authentication data</returns>
    public async Task<AuthData> Execute(CancellationToken cancellationToken)
    {
        var authData = await _rocketChatStorage.GetAuthData(cancellationToken);

        if (authData is not null)
            return authData;

        var loginRequest = new LoginRequest(_rocketChatApiSettings.User!, _rocketChatApiSettings.Password!);
        var loginResponse = await _apiClient.GetMethod<LoginMethod>()
            .ExecuteAsync(new BaseApiRequest<LoginRequest>(loginRequest), cancellationToken);

        if (!loginResponse.IsSuccess || loginResponse.Content?.Status != LoginStatus.Success)
            throw new UnauthorizedAccessException(loginResponse.ErrorMessage);

        authData = new AuthData(loginResponse.Content.LoginData.UserId,
            loginResponse.Content.LoginData.AuthToken);

        await _rocketChatStorage.SetAuthData(authData, cancellationToken);

        return authData;
    }
}