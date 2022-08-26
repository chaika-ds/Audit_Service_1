using AuditService.Common.Enums;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Models;
using AuditService.Tests.Fakes.ServiceData;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.Localization.Localizer;

/// <summary>
///     Localizer test
/// </summary>
public class LocalizerTest
{
    private readonly ILocalizer _localizer;
    private const string NotExistingKey = "KeyDoNotExist";

    /// <summary>
    ///     Initialize Localizer
    /// </summary>
    public LocalizerTest()
    {
        var serviceCollectionFake = ServiceProviderFake.GetServiceProviderForLocalization();

        _localizer = serviceCollectionFake.GetRequiredService<ILocalizer>();
    }

    /// <summary>
    ///     Test for TryLocalize method with multiple key
    /// </summary>
    [Fact]
    public async Task TryLocalize_MultipleKeys_Return_LocalizationsKeysAndValuesAsync()
    {
        var request = new LocalizeKeysRequest(ModuleName.BI, "en", new List<string>() {nameof(ModuleName.BI), NotExistingKey});

        var result = await _localizer.TryLocalize(request, CancellationToken.None);

        result.TryGetValue(nameof(ModuleName.BI), out var value);
        result.TryGetValue(NotExistingKey, out var valueNotExist);

        NotNull(result);
        NotEqual(nameof(ModuleName.BI), value);
        Equal(NotExistingKey, valueNotExist);
    }

    /// <summary>
    ///     Test for TryLocalize method with single key
    /// </summary>
    [Fact]
    public async Task TryLocalize_SingleKey_Return_LocalizedValueAsync()
    {
        var request = new LocalizeKeyRequest(ModuleName.BI, "en", nameof(ModuleName.BI));

        var result = await _localizer.TryLocalize(request, CancellationToken.None);

        NotNull(result);
        NotEqual(nameof(ModuleName.BI), result);
    }
    
    /// <summary>
    ///     Test for TryLocalize method with unlocalized key
    /// </summary>
    [Fact]
    public async Task TryLocalize_NotExistingKey_Return_KeyAsync()
    {
        var request = new LocalizeKeyRequest(ModuleName.BI, "en", NotExistingKey);

        var result = await _localizer.TryLocalize(request, CancellationToken.None);

        NotNull(result);
        Equal(NotExistingKey, result);
    }
}