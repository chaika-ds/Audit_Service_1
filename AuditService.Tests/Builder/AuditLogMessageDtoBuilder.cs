using AuditService.Common;
using AuditService.Data.Domain.Dto;

namespace AuditService.Tests;

public class AuditLogMessageDtoBuilder : BuilderBase<AuditLogMessageDto>
{
    public AuditLogMessageDtoBuilder()
        : base()
    {
    }

    public override AuditLogMessageDto Get()
    {
        var result = base.Get();
        result.OldValue = Helper.GetJson(@"JsonModels/OldValueEntity.json");
        result.NewValue = Helper.GetJson(@"JsonModels/NewValueEntity.json");

        return result;
    }
}

