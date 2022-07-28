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
        if (setUp is null)
        {
            _serviceCollection.AddTransient<IConsumer, TConsumer>();
            return;
        }

        var consumerSettings = new ConsumerSettings();
        setUp.Invoke(consumerSettings);

        if (!NeedRegisterConsumer(consumerSettings))
            return;

        for (var i = 0; i < consumerSettings.LaunchedCounts; i++)
            _serviceCollection.AddTransient<IConsumer, TConsumer>();
    }

    /// <summary>
    ///     Check if the consumer needs to be register
    /// </summary>
    /// <param name="consumerSettings">Consumer settings</param>
    /// <returns>Need to register</returns>
    private bool NeedRegisterConsumer(ConsumerSettings consumerSettings) => 
        consumerSettings.AvailableEnvironments == null || consumerSettings.AvailableEnvironments.Any(env =>
        string.Equals(env, _environmentName, StringComparison.CurrentCultureIgnoreCase));
}