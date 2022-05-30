namespace AuditService.Data.Domain.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public Guid NodeId { get; set; }
    public List<Guid> NodeIds { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Nickname { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int AccessLevel { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public bool AwaitsEmailVerification { get; set; }
    public bool AwaitsPhoneVerification { get; set; }
}