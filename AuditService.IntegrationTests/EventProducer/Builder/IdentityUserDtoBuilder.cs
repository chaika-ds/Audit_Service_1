using AuditService.Data.Domain.Dto;

namespace AuditService.IntegrationTests
{
    public class IdentityUserDtoBuilder : BuilderBase<IdentityUserDto>
    {

        public IdentityUserDtoBuilder()
       : base()
        {
        }

        public override IdentityUserDto Get()
        {
            var result = base.Get();
            return result;
        }
    }
}
