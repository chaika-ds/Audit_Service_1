using AuditService.Common;
using AuditService.Data.Domain.Dto;

namespace AuditService.EventProducer;

public class AuditLogMessageDtoBuilder : BuilderBase<AuditLogMessageDto>
{
    public AuditLogMessageDtoBuilder()
        : base()
    {
    }

    public override AuditLogMessageDto Get()
    {
        var result = base.Get();
        result.OldValue = Helper.GetJson("OldValueEntity.json");
        result.NewValue = Helper.GetJson("NewValueEntity.json");

        return result;
    }
}

