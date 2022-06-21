using AuditService.Common.Models.Domain;

namespace AuditService.IntegrationTests.EventProducer.Builder
{
    public class IdentityUserDtoBuilder : BuilderBase<IdentityUserDomainModel>
    {

        public IdentityUserDtoBuilder()
       : base()
        {
        }

        public override IdentityUserDomainModel Get()
        {
            var result = base.Get();
            return result;
        }
    }
}
