namespace KIT.Kafka.BackgroundServices.Runner.RunningRegistrar;

/// <summary>
///     Consumer running settings
/// </summary>
internal class ConsumerSettings
{
    /// <summary>
    ///     Environments on which consumers will run
    /// </summary>
    public IList<string>? AvailableEnvironments { get; set; }

    /// <summary>
    ///     Number of concurrently running consumers
    /// </summary>
    public int LaunchedCounts { get; set; } = 1;

    /// <summary>
    ///     Run consumers for environments
    /// </summary>
    /// <param name="environments">Run consumers for environments</param>
    public void RunForEnvironments(params string[] environments)
    {
        AvailableEnvironments = environments;
    }
}