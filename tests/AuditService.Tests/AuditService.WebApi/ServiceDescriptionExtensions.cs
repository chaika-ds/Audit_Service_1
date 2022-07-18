using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.WebApi
{
    public static class ServiceDescriptionExtensions
    {
        public static bool Is<TService, TInstance>(this ServiceDescriptor serviceDescriptor, ServiceLifetime lifetime)
        {
            var cc = serviceDescriptor.ServiceType == typeof(TService) &&
                   serviceDescriptor.ImplementationType == typeof(TInstance) &&
                   serviceDescriptor.Lifetime == lifetime;
            return cc;
        }
    }
}
