namespace AuditService.Common.Attributes;

/// <summary>
///     Attribute for working with localization
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class LocalizationAttribute : Attribute
{
    /// <summary>
    ///     Key for localization service
    /// </summary>
    public string Key { get; set; } = null!;
}