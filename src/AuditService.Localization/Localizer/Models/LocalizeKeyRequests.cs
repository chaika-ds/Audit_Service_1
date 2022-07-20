using AuditService.Common.Enums;

namespace AuditService.Localization.Localizer.Models;

/// <summary>
///     Request for localization by keywords
/// </summary>
/// <param name="Module">Module name</param>
/// <param name="Language">Language for localization</param>
/// <param name="Keys">List of keys for localization</param>
public record LocalizeKeysRequest(ModuleName Module, string? Language, IList<string> Keys);

/// <summary>
///     Request for localization by keyword
/// </summary>
/// <param name="Module">Module name</param>
/// <param name="Language">Language for localization</param>
/// <param name="Key">Key for localization</param>
public record LocalizeKeyRequest(ModuleName Module, string? Language, string Key);

