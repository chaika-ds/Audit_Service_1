using AuditService.Common.Helpers;
using AuditService.Data.Domain.Dto;

namespace AuditService.IntegrationTests.EventProducer.Builder;

public class AuditLogMessageDtoBuilder : BuilderBase<AuditLogTransactionDto>
{
    public AuditLogMessageDtoBuilder()
        : base()
    {
    }

    public override AuditLogTransactionDto Get()
    {
        var result = base.Get();
        result.OldValue = JsonHelper.GetJson(@"JsonModels/OldValueEntity.json");
        result.NewValue = JsonHelper.GetJson(@"JsonModels/NewValueEntity.json");

        var identityUserDto = new IdentityUserDtoBuilder();
        result.User = identityUserDto.Get();

        return result;
    }
}

