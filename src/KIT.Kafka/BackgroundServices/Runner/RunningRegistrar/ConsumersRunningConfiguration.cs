using KIT.Kafka.Consumers.Base;
using Microsoft.Extensions.DependencyInjection;

namespace KIT.Kafka.BackgroundServices.Runner.RunningRegistrar;

/// <summary>
///     Consumers running configuration
/// </summary>
internal class ConsumersRunningConfiguration
{
    private readonly string _environmentName;
    private readonly IServiceCollection _serviceCollection;

    public ConsumersRunningConfiguration(IServiceCollection serviceCollection, string environmentName)
    {
        _serviceCollection = serviceCollection;
        _environmentName = environmentName;
    }

    /// <summary>
    ///     Register consumer to run
    /// </summary>
    /// <typeparam name="TConsumer">Consumer type</typeparam>
    /// <param name="setUp">Consumer setup action</param>
    public void Consumer<TConsumer>(Action<ConsumerSettings>? setUp = null) where TConsumer : class, IConsumer
    {
        if (NeedRegisterConsumer(setUp))
            _serviceCollection.AddSingleton<IConsumer, TConsumer>();
    }

    /// <summary>
    ///     Check if the consumer needs to be register
    /// </summary>
    /// <param name="setUp">Consumer setup action</param>
    /// <returns>Need to register</returns>
    private bool NeedRegisterConsumer(Action<ConsumerSettings>? setUp = null)
    {
        if (setUp is null)
            return true;

        var consumerSettings = new ConsumerSettings();
        setUp.Invoke(consumerSettings);

        return consumerSettings.AvailableEnvironments == null || consumerSettings.AvailableEnvironments.Any(env =>
            string.Equals(env, _environmentName, StringComparison.CurrentCultureIgnoreCase));
    }
}