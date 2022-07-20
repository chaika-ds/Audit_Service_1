namespace AuditService.Localization.Settings
{
    /// <summary>
    ///     Settings for resource localization source
    /// </summary>
    public interface ILocalizationSourceSettings
    {
        /// <summary>
        ///     Uri template for downloading localization resources
        /// </summary>
        string? UriTemplate { get; set; }
    }
}
