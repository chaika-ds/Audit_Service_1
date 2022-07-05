namespace KIT.Kafka.Consumers.RunningRegistrar;

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
    ///     Run consumers for environments
    /// </summary>
    /// <param name="environments">Run consumers for environments</param>
    public void RunForEnvironments(params string[] environments)
    {
        AvailableEnvironments = environments;
    }
}