namespace AuditService.Common.Attributes;

/// <summary>
///     An attribute indicating that the request header will be used(in Swagger)
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class UseHeaderAttribute : Attribute
{
    /// <summary>
    ///     Header name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Header is required
    /// </summary>
    public bool IsRequired { get; set; } = false;

}