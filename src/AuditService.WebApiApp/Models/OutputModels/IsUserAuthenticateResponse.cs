using AuditService.Data.Domain.Dto;

namespace AuditService.WebApiApp.Models.OutputModels;

public class IsUserAuthenticateResponse
{
    /// <summary>
    /// Status
    /// </summary>
    public int Status { get; set; }
    
    /// <summary>
    /// TokenLifeTimeInMinutes
    /// </summary>
    public int TokenLifeTimeInMinutes { get; set; }
    
    /// <summary>
    /// User info
    /// </summary>
    public UserDto UserDto { get; set; }
    
    /// <summary>
    /// IssuedAt
    /// </summary>
    public DateTime IssuedAt { get; set; }
    
    /// <summary>
    /// Permissions
    /// </summary>
    public List<string> Permissions { get; set; }
}

