using AuditService.Common;
using AuditService.Data.Domain.Domain;
using AuditService.Data.Domain.Dto;

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
        result.OldValue = Helper.GetJson(@"JsonModels/OldValueEntity.json");
        result.NewValue = Helper.GetJson(@"JsonModels/NewValueEntity.json");

        var identityUserDto = new IdentityUserDtoBuilder();
        result.User = identityUserDto.Get();

        return result;
    }
}

