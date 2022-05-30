namespace AuditService.EventProducer;
    public interface IDirector
{
    Task<T> GenerateDto<T>(int count = 1)
        where T : class;

    Task<T> SendDto<T>(T dto)
        where T : class;
}

