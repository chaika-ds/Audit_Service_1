using Microsoft.Extensions.DependencyInjection;

namespace AuditService.SettingsService.Commands.BaseEntities;

/// <summary>
///     Settings service commands
/// </summary>
public class SettingsServiceCommands
{
    private readonly IServiceProvider _serviceProvider;

    public SettingsServiceCommands(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Get settings service command
    /// </summary>
    /// <typeparam name="TCommand">Type of command</typeparam>
    /// <returns>Settings service command</returns>
    public TCommand GetCommand<TCommand>() where TCommand : ISettingsServiceCommand
        => _serviceProvider.GetRequiredService<TCommand>();
}