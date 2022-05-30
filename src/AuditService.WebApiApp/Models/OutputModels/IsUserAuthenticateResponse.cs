using AuditService.Data.Domain.Dto;

namespace AuditService.WebApiApp.Models.OutputModels;

public class IsUserAuthenticateResponse
{
    public int Status { get; set; }
    public int TokenLifeTimeInMinutes { get; set; }
    public UserDto UserDto { get; set; }
    public DateTime IssuedAt { get; set; }
    public List<string> Permissions { get; set; }
}

