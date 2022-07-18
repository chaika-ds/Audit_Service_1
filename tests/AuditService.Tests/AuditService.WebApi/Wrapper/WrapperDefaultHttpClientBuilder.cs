using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi.Wrapper
{
    public class WrapperDefaultHttpClientBuilder : IHttpClientBuilder
    {
        public WrapperDefaultHttpClientBuilder(IServiceCollection services, string name)
        {
            Services = services;
            Name = name;
        }

        public string Name { get; }

        public IServiceCollection Services { get; }
    }
}
