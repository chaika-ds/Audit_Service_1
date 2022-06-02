using System.Threading.Tasks;

namespace AuditService.IntegrationTests.EventProducer.Builder;
    public interface IDirector
{
    Task GenerateDto<T>(int count = 1)
        where T : class;

    Task<T> SendDto<T>(T dto)
        where T : class;
}

