using AuditService.Common.Models.Domain;
using AuditService.Localization.Localizer.Triggers;
using AuditService.Tests.Fakes.ServiceData;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.Localization.Localizer.Triggers;

public class OnLocalizationChangesTriggerTest
{
    
    private readonly IOnLocalizationChangesTrigger _onLocalizationChangesTrigger;

    /// <summary>
    ///     Initialize Localizer
    /// </summary>
    public OnLocalizationChangesTriggerTest()
    {
        var serviceCollectionFake = ServiceProviderFake.GetServiceProviderForLocalization();

        _onLocalizationChangesTrigger = serviceCollectionFake.GetRequiredService<IOnLocalizationChangesTrigger>();
    }

    /// <summary>
    ///     Test for TryLocalize method with multiple key
    /// </summary>
    [Fact]
    public async Task PushChangesAsync()
    {
        var model = new LocalizationChangedDomainModel();
        
        await _onLocalizationChangesTrigger.PushChangesAsync(model, CancellationToken.None);
    }
}