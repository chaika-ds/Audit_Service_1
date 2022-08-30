using AuditService.Tests.Fakes.ServiceData;
using KIT.RocketChat.Commands.PostBufferedTextMessage;
using KIT.RocketChat.Commands.PostBufferedTextMessage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.RocketChat
{
    /// <summary>
    ///     RockeChat test
    /// </summary>
    public class RocketChatTest
    {
        /// <summary>
        ///     Fake string value
        /// </summary>
        private const string stringValueFake = "test";

        /// <summary>
        ///     Check if PostBufferMessageCommand sending message
        /// </summary>
        [Fact]
        public async Task ExecutingPostBufferMessageCommand_CreateEnviroment_True()
        {
            //Act
            var result = await GetPostMessageResult(true);

            //Assert
            True(result);
        }

        /// <summary>
        ///     Check if PostBufferMessageCommand blocked sending message
        /// </summary>
        [Fact]
        public async Task ExecutingPostBufferMessageCommand_CreateEnviroment_False()
        {
            //Act
            var result = await GetPostMessageResult(false);

            //Assert
            False(result);
        }

        /// <summary>
        ///     Check if processing of exception  in PostBufferMessageCommand
        /// </summary>
        [Fact]
        public async Task ProcessingOfExcludedPostBufferMessageCommand_CreateEnviroment_False()
        {
            //Arrange
            var serviceProvider = ServiceProviderFake.GetThrowExceptionServiceProviderForRocketChat();

            var command = serviceProvider.GetRequiredService<IPostBufferedMessageCommand>();

            var request = new PostBufferedMessageRequest(stringValueFake, stringValueFake, stringValueFake);

            //Act
            var result = await command.Execute(request);

            //Assert
            False(result);
        }

        /// <summary>
        ///     Get post message result
        /// </summary>
        /// <param name="IsActive">is active command</param>
        /// <returns>status result</returns>
        private async Task<bool> GetPostMessageResult(bool IsActive)
        {
            var serviceProvider = ServiceProviderFake.GetServiceProviderForRocketChat(IsActive);

            var command = serviceProvider.GetRequiredService<IPostBufferedMessageCommand>();

            var request = new PostBufferedMessageRequest(stringValueFake, stringValueFake, stringValueFake);

            var result = await command.Execute(request);

            return result;
        }
    }
}
