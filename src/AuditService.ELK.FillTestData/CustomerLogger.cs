using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuditService.ELK.FillTestData
{
    internal class CustomerLogger
    {
        ///// <summary>
        ///// Adds customer logger provider at <paramref name="environmentName"/> to <paramref name="builder"/>.
        ///// </summary>
        ///// <param name="builder"></param>
        ///// <param name="environmentName"></param>
        //public void AddCustomerLogger(ConfigurationBuilder builder, string environmentName)
        //{
        //    builder.Logging.ClearProviders();
        //    builder.Logging.SetMinimumLevel(LogLevel.Trace);
        //    builder.Logging.AddAuditServiceLogger(options => {
        //        builder.Configuration.Bind(options);
        //        options.Channel = LogChannelParsing.CheckAndParseChannel(environmentName.ToLower());
        //    });
        //}        
    }
}
