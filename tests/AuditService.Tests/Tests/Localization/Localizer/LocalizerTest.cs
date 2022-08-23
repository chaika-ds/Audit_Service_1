using AuditService.Common.Enums;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Models;
using AuditService.Tests.Fakes.ServiceData;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.Localization.Localizer;

public class LocalizerTest
{
    public readonly ILocalizer _Localizer;
    
    public LocalizerTest()
    {
        var  serviceCollectionFake = ServiceProviderFake.GetServiceProviderForLocalization();
        
        _Localizer = serviceCollectionFake.GetRequiredService<ILocalizer>();
    }
    
    [Fact]
    public async void TryLocalizeTestAsync()
    {
        var request = new LocalizeKeysRequest(ModuleName.BI, "en", new List<string>());
        await _Localizer.TryLocalize(request, CancellationToken.None);
    }
}