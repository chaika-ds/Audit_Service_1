namespace AuditService.Common.Services
{
    public interface IInputSettings<T>
    {
        string Name { get; set; }

        string Topic { get; set; }
    }
}
