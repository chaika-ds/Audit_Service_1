using AuditService.Common.Models.Domain;
using AuditService.Localization.Localizer.Triggers;
using AuditService.Tests.Fakes.Localization;
using AuditService.Tests.Fakes.ServiceData;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.Tests.Localization.Localizer.Triggers;

public class OnLocalizationChangesTriggerTest
{
    private readonly IOnLocalizationChangesTrigger _onLocalizationChangesTrigger;

    /// <summary>
    ///     Initialize OnLocalizationChangesTrigger
    /// </summary>
    public OnLocalizationChangesTriggerTest()
    {
        var serviceCollectionFake = ServiceProviderFake.GetServiceProviderForLocalization();

        _onLocalizationChangesTrigger = serviceCollectionFake.GetRequiredService<IOnLocalizationChangesTrigger>();
    }

    /// <summary>
    ///     Test for PushChangesAsync check ClearResources called with expected values
    /// </summary>
    [Fact]
    public async Task PushChangesAsync_Check_ClearResources_Called_WithExpectedValuesAsync()
    {
        //Arrange
        var model = new LocalizationChangedDomainModel()
        {
            Action = "Create",
            Service = "BI",
            Text = new List<Dictionary<string, string>>()
            {
                new() {["TestKey1"] = "Test Key 1"},
                new() {["TestKey2"] = "Test Key 2"},
                new() {["TestKey3"] = "Test Key 3"},
            },
            Type = "change"
        };

        //Act
        await _onLocalizationChangesTrigger.PushChangesAsync(model, CancellationToken.None);

        var keys = model.Text.SelectMany(changedFields => changedFields.Keys).Distinct().ToList();

        //Assert
        Equal(GenerateService(model), RedisCacheStorageFake.LocalizationResourceParameters.Service);
        Equal(keys.Count, RedisCacheStorageFake.Languages.Count);
        Equal(keys.Count, RedisCacheStorageFake.ClearResourcesCalled);
    }

    /// <summary>
    ///     Test for PushChangesAsync remove duplicate keys
    /// </summary>
    [Fact]
    public async Task PushChangesAsync_Check_Duplicate_Key_Selected_DistinctAsync()
    {
        //Arrange
        const int distinctKeyCount = 2;
        
        var model = new LocalizationChangedDomainModel()
        {
            Action = "Create",
            Service = "BI",
            Text = new List<Dictionary<string, string>>()
            {
                new() {["TestKey1"] = "Test Key 1"},
                new() {["TestKey1"] = "Test Key 2"},
                new() {["TestKey2"] = "Test Key 3"},
                new() {["TestKey2"] = "Test Key 3"},
            },
            Type = "change"
        };

        //Act
        await _onLocalizationChangesTrigger.PushChangesAsync(model, CancellationToken.None);

        //Assert 
        Equal(distinctKeyCount, RedisCacheStorageFake.Languages.Count);
        Equal(distinctKeyCount, RedisCacheStorageFake.ClearResourcesCalled);
    }

    /// <summary>
    ///     Generate service for localization
    /// </summary>
    /// <param name="model">Localization changed model</param>
    /// <returns>Service name</returns> 
    private static string GenerateService(LocalizationChangedDomainModel model) => $"{model.Service}-{model.Type}";
}