using Moq;

namespace AuditService.Tests.Factories.Interfaces;

/// <summary>
///     Mock factory interface
/// </summary>
public interface IMockFactory
{
    public Mock CreateMockObject<TModel>(IEnumerable<IBaseMock> input);
}

