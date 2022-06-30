using System.IO;
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
        
        using var streamReaderOldValue = new StreamReader(@"JsonModels/OldValueEntity.json");
        result.OldValue =  streamReaderOldValue.ReadToEnd();
        
        using var streamReaderNewValue = new StreamReader(@"JsonModels/NewValueEntity.json");
        result.NewValue =  streamReaderNewValue.ReadToEnd();

        var identityUserDto = new IdentityUserDtoBuilder();
        result.User = identityUserDto.Get();

        return result;
    }
}

