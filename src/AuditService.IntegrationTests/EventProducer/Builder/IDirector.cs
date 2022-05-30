using System.Threading.Tasks;

namespace AuditService.IntegrationTests;
    public interface IDirector
{
    Task<T> GenerateDto<T>(int count = 1)
        where T : class;

    Task<T> SendDto<T>(T dto)
        where T : class;
}

