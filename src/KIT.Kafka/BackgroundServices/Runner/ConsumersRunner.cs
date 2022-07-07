using KIT.Kafka.Consumers.Base;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KIT.Kafka.BackgroundServices.Runner;

/// <summary>
///     Background service to start consumers
/// </summary>
public class ConsumersRunner : IHostedService
{
    private readonly List<IConsumer> _consumers;
    private readonly ILogger<ConsumersRunner> _logger;

    public ConsumersRunner(IEnumerable<IConsumer> consumers, ILogger<ConsumersRunner> logger)
    {
        _consumers = consumers.ToList();
        _logger = logger;
    }

    /// <summary>
    ///     Start an background service to start consumers
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumers.ForEach(StartConsumer);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Stop an background service(stop consumers)
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumers.ForEach(StopConsumer);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Start message consumer
    /// </summary>
    /// <param name="consumer">Kafka message consumer</param>
    private void StartConsumer(IConsumer consumer)
    {
        _logger.LogInformation("{msg}", $"{consumer.Name} starting");

        try
        {
            consumer.Start();
            _logger.LogInformation("{msg}", $"{consumer.Name} started");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{msg} {error}", "Error occurred while starting consumer",
                $"Unable to start {consumer.Name}");
        }
    }

    /// <summary>
    ///     Stop message consumer
    /// </summary>
    /// <param name="consumer">Kafka message consumer</param>
    private void StopConsumer(IConsumer consumer)
    {
        _logger.LogInformation("{msg}", $"{consumer.Name} stopping");

        try
        {
            consumer.Stop();
            _logger.LogInformation("{msg}", $"{consumer.Name} stopped");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{msg} {error}", "Error occured while stopping consumer", ex.Message);
        }
    }
}