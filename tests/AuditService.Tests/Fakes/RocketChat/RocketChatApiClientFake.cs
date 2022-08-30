using KIT.RocketChat.ApiClient;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.GetRooms;
using KIT.RocketChat.ApiClient.Methods.GetRooms.Models;
using KIT.RocketChat.ApiClient.Methods.PostMessage;
using KIT.RocketChat.ApiClient.Methods.PostMessage.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RestSharp;
using System.Net;

namespace AuditService.Tests.Fakes.RocketChat
{
    /// <summary>
    ///     RocketChat client api fake
    /// </summary>
    public class RocketChatApiClientFake : IRocketChatApiClient
    {
        /// <summary>
        ///     Fake string value
        /// </summary>
        private const string stringValueFake = "test";

        /// <summary>
        ///     Methods dictionary
        /// </summary>
        private readonly Dictionary<Type, Func<IBaseMethod>> methods;

        /// <summary>
        ///     Service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        public RocketChatApiClientFake(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            methods = new Dictionary<Type, Func<IBaseMethod>>
            {
                { typeof(GetRoomsMethod), GetGetRoomsMethod},
                { typeof(PostMessageMethod), GetPostMessageMethod}
            };
        }

        /// <summary>
        ///     Get fake api method
        /// </summary>
        /// <typeparam name="TMethod">Method type</typeparam>
        /// <returns>Api method</returns>
        public TMethod GetMethod<TMethod>() where TMethod : IBaseMethod
        {
            var typeOfMethod = typeof(TMethod);

            if (methods.TryGetValue(typeOfMethod, out var getMethod))
            {
                return (TMethod)getMethod();
            }

            return _serviceProvider.GetRequiredService<TMethod>();
        }

        /// <summary>
        ///      Get GetRoomsMethod
        /// </summary>
        /// <returns>GetRoomsMethod</returns>
        private GetRoomsMethod GetGetRoomsMethod()
        {
            var rooms = new List<Room>
                {
                    new Room()
                    {
                        Name = stringValueFake,
                        Id = stringValueFake
                    }
                };

            var getRoomsResponse = new GetRoomsResponse(rooms, new List<object>(), true);

            var result = new BaseApiResponse<GetRoomsResponse>(getRoomsResponse, HttpStatusCode.OK, ResponseStatus.None);

            var mockGetRoopsMethod = new Mock<GetRoomsMethod>(_serviceProvider);

            mockGetRoopsMethod.
                Setup(x => x.ExecuteAsync(It.IsAny<BaseApiRequest<GetRoomsRequest>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(result));

            var getRoomMethod = mockGetRoopsMethod.Object;

            return getRoomMethod;
        }

        /// <summary>
        ///      Get PostMessageMethod
        /// </summary>
        /// <returns>PostMessageMethod</returns>
        private PostMessageMethod GetPostMessageMethod()
        {
            var message = new Message(stringValueFake);

            var postMessageResponse = new PostMessageResponse(stringValueFake, stringValueFake, message, true);

            var result = new BaseApiResponse<PostMessageResponse>(postMessageResponse, HttpStatusCode.OK, ResponseStatus.None);

            var mockGetRoopsMethod = new Mock<PostMessageMethod>(_serviceProvider);

            mockGetRoopsMethod
                .Setup(x => x.ExecuteAsync(It.IsAny<BaseApiRequest<PostMessageRequest>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(result));

            var postMessageMethod = mockGetRoopsMethod.Object;

            return postMessageMethod;
        }
    }
}
