using System.Linq.Expressions;

namespace AuditService.Common.Services
{
    public interface IOutputService
    {
        Task SaveAsync<T>(T obj, Expression<Func<T, bool>> findPredicate)
           where T : class, new();

        Task MapToEntityAndSaveAsync<TInput, TOutput>(TInput inputObject)
            where TInput : class, new()
            where TOutput : class, new();
    }
}
