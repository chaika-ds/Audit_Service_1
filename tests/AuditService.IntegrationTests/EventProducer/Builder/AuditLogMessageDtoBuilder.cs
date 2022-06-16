using AuditService.Common.Models.Domain;
using AuditService.Utility.Helpers;

namespace AuditService.IntegrationTests.EventProducer.Builder;

public class AuditLogMessageDtoBuilder : BuilderBase<AuditLogTransactionDomainModel>
{
    public AuditLogMessageDtoBuilder()
        : base()
    {
    }

    public override AuditLogTransactionDomainModel Get()
    {
        var result = base.Get();
        result.OldValue = JsonHelper.GetJson(@"JsonModels/OldValueEntity.json");
        result.NewValue = JsonHelper.GetJson(@"JsonModels/NewValueEntity.json");

        var identityUserDto = new IdentityUserDtoBuilder();
        result.User = identityUserDto.Get();

        return result;
    }
}

