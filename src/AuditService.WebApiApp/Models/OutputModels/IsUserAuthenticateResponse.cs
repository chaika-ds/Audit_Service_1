namespace AuditService.WebApiApp.Models.OutputModels;

public abstract class IsUserAuthenticateResponse
{
    public int Status { get; set; }
    public int TokenLifeTimeInMinutes { get; set; }
    public User User { get; set; }
    public DateTime IssuedAt { get; set; }
    public List<string> Permissions { get; set; }
}

public class User
{
    public Guid Id { get; set; }
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