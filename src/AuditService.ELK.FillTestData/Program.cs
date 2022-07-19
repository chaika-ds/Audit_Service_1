using AuditService.ELK.FillTestData.Extensions;
using AuditService.ELK.FillTestData.Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var appBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, cfg) =>
        {            cfg.ConfigureAppConfiguration(hostingContext.HostingEnvironment);
        }).ConfigureServices(services => { services.RegisterAppServices(); });

var host = appBuilder.Build();
using var scope = host.Services.CreateScope();

scope.ServiceProvider.GetRequiredService<AuditLogGenerator>();

await host.RunAsync();
