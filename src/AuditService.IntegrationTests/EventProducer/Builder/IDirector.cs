using System.Threading.Tasks;

namespace AuditService.IntegrationTests.EventProducer.Builder;
    public interface IDirector
{
    Task GenerateDtoAsync<T>(int count = 1)
        where T : class;

    Task<T> SendDtoAsync<T>(T dto)
        where T : class;
}

