using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace AuditService.Tests.AuditService.WebApi.HttpClientMock
{
    public class ConfigurationFake : IConfiguration
    {
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public string this[string key]
        {
            get => "test";
            set => SetValue = key;
        }

        private string SetValue { get; set; }
    }
}
