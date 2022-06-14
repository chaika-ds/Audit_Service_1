namespace AuditService.Data.Domain.Dto;

/// <summary>
/// Permissions from SSO Permissions/All
/// </summary>
public class PermissionDto
{
    public int id { get; set; }

    public string systemName { get; set; }

    public string description { get; set; }

    public int? serviceName { get; set; }
}

