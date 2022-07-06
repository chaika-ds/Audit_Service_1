using Microsoft.Extensions.DependencyInjection;

namespace KIT.Kafka.BackgroundServices.Runner.RunningRegistrar;

/// <summary>
///     The consumer registrar to run in the background service.
/// </summary>
internal static class ConsumersRunningRegistrar
{
    /// <summary>
    ///     Register consumers runner
    /// </summary>
    /// <param name="services">Services сollection</param>
    /// <param name="configure">Consumers running configuration action</param>
    /// <param name="environmentName">The name of the current environment</param>
    public static void RegisterСonsumersRunner(this IServiceCollection services,
        Action<ConsumersRunningConfiguration> configure, string environmentName)
    {
        configure(new ConsumersRunningConfiguration(services, environmentName));
        services.AddHostedService<ConsumersRunner>();
    }
}