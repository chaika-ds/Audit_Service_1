using Moq;

namespace AuditService.Tests.TRASH.Interfaces;

/// <summary>
///     Mock factory interface
/// </summary>
public interface IMockFactory
{
    public Mock CreateMockObject<TModel>(IEnumerable<IBaseMock> input);
}

