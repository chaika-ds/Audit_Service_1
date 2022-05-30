namespace AuditService.Data.Domain.Dto;

public class UserDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Id ноды.
    /// </summary>
    public Guid NodeId { get; set; }
    
    /// <summary>
    /// Ids of the nodes to which the user is bound
    /// </summary>
    public List<Guid> NodeIds { get; set; }
    
    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// LastName.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Login.
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Nickname.
    /// </summary>
    public string Nickname { get; set; }
    
    /// <summary>
    /// Date of registration
    /// </summary>
    public DateTime RegistrationDate { get; set; }
    
    /// <summary>
    /// AccessLevel
    /// </summary>
    public int AccessLevel { get; set; }
    
    /// <summary>
    /// Status
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// AwaitsEmailVerification
    /// </summary>
    public bool AwaitsEmailVerification { get; set; }
    
    /// <summary>
    /// AwaitsPhoneVerification
    /// </summary>
    public bool AwaitsPhoneVerification { get; set; }
}