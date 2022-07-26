using KIT.NLog.Extensions;
using KIT.RocketChat.ApiClient;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.GetRooms;
using KIT.RocketChat.ApiClient.Methods.GetRooms.Models;
using KIT.RocketChat.ApiClient.Methods.PostMessage;
using KIT.RocketChat.ApiClient.Methods.PostMessage.Models;
using KIT.RocketChat.Commands.Authorization;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using KIT.RocketChat.Settings.Interfaces;
using KIT.RocketChat.Storage;
using Microsoft.Extensions.Logging;

namespace KIT.RocketChat.Commands.PostBufferedTextMessage;

/// <summary>
///     RocketChat command to post buffered message
/// </summary>
internal class PostBufferedMessageCommand : IPostBufferedMessageCommand
{
    private readonly IRocketChatApiClient _apiClient;
    private readonly IAuthorizationCommand _authorizationCommand;
    private readonly ILogger<PostBufferedMessageCommand> _logger;
    private readonly IRocketChatApiSettings _rocketChatApiSettings;
    private readonly IRocketChatStorage _rocketChatStorage;

    public PostBufferedMessageCommand(IAuthorizationCommand authorizationCommand,
        IRocketChatStorage rocketChatStorage, IRocketChatApiClient apiClient,
        ILogger<PostBufferedMessageCommand> logger,
        IRocketChatApiSettings rocketChatApiSettings)
    {
        _authorizationCommand = authorizationCommand;
        _rocketChatStorage = rocketChatStorage;
        _apiClient = apiClient;
        _logger = logger;
        _rocketChatApiSettings = rocketChatApiSettings;
    }

    /// <summary>
    ///     Execute a command
    /// </summary>
    /// <param name="request">Request model for post a buffered message in a thread</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    public async Task<bool> Execute(PostBufferedMessageRequest request, CancellationToken cancellationToken = default)
    {
        if (_rocketChatApiSettings.IsActive! == false)
            return false;

        try
        {
            if (await _rocketChatStorage.GetBufferedMessage(request.BufferKey, cancellationToken) is var bufferedMessage 
                && bufferedMessage is not null && bufferedMessage.IsBlockedMessage)
                return false;

            var authData = await _authorizationCommand.Execute(cancellationToken);
            var roomId = await FindRoomId(request.RoomName, authData, cancellationToken);

            if (roomId is null)
                return false;
            
            var messageId = await TryPostMessage(authData, request.Message, roomId, bufferedMessage?.MessageId, cancellationToken);

            if (messageId is null)
                return false;

            await ProcessBufferedMessage(bufferedMessage, messageId, request.BufferKey, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, contextModel: request);
            return false;
        }
    }

    /// <summary>
    ///     Process buffered message after sending.
    ///     If there is no buffered message - create.
    /// </summary>
    /// <param name="bufferedMessage">Buffered message</param>
    /// <param name="messageId">Message Id</param>
    /// <param name="bufferKey">Buffer key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result</returns>
    private async Task ProcessBufferedMessage(BufferedMessage? bufferedMessage, string messageId, string bufferKey, CancellationToken cancellationToken)
    {
        if (bufferedMessage is null)
        {
            await _rocketChatStorage.SetBufferedMessage(new BufferedMessage(messageId, bufferKey), cancellationToken);
            return;
        }

        await _rocketChatStorage.SetBlockOnBufferedMessage(bufferedMessage.MessageId, cancellationToken);
    }

    /// <summary>
    ///     Try post message
    /// </summary>
    /// <param name="authData">Auth data</param>
    /// <param name="message">Message to post</param>
    /// <param name="roomId">Room Id</param>
    /// <param name="threadMessageId">Thread message Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Message Id(If not successful then null)</returns>
    private async Task<string?> TryPostMessage(AuthData authData, string message, string roomId,
        string? threadMessageId, CancellationToken cancellationToken)
    {
        var postMessageRequest = new BaseApiRequest<PostMessageRequest>(new PostMessageRequest
        {
            Text = message,
            RoomId = roomId,
            ThreadMessageId = threadMessageId
        }, authData);

        var postMessageResponse = await _apiClient.GetMethod<PostMessageMethod>()
            .ExecuteAsync(postMessageRequest, cancellationToken);

        if (!postMessageResponse.IsSuccess || postMessageResponse.Content is null ||
            !postMessageResponse.Content.IsSuccess)
            return null;

        return postMessageResponse.Content.Message.Id;
    }

    /// <summary>
    ///     Find room Id by name
    /// </summary>
    /// <param name="roomName">Room name</param>
    /// <param name="authData">Auth data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Room Id</returns>
    private async Task<string?> FindRoomId(string roomName, AuthData authData, CancellationToken cancellationToken)
    {
        var apiResponse = await _apiClient.GetMethod<GetRoomsMethod>()
            .ExecuteAsync(new BaseApiRequest<GetRoomsRequest>(new GetRoomsRequest(), authData), cancellationToken);

        if (!apiResponse.IsSuccess || apiResponse.Content is null || !apiResponse.Content.IsSuccess)
            return null;

        var room = apiResponse.Content.Rooms.FirstOrDefault(room => room.Name == roomName || room.Fname == roomName);

        if (room != null) return room.Id;

        _logger.LogError($"Unable to find room by name - {roomName}", roomName);
        return null;
    }
}