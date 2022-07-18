using AuditService.ELK.FillTestData;
using AuditService.ELK.FillTestData.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var appBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, cfg) =>
        {            cfg.ConfigureAppConfiguration(hostingContext.HostingEnvironment);
        }).ConfigureServices(services => { services.RegisterAppServices(); });

var host = appBuilder.Build();
using var scope = host.Services.CreateScope();
await scope.ServiceProvider.GetRequiredService<ElasticSearchDataFiller>().ExecuteAsync(); 

scope.ServiceProvider.GetRequiredService<AuditLogGenerator>();

await host.RunAsync();
