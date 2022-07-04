using AuditService.ELK.FillTestData;
using AuditService.ELK.FillTestData.Extensions;
using AuditService.Utility.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


    var appBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, cfg) =>
        {
            cfg.ConfigureAppConfiguration(hostingContext.HostingEnvironment);
        }).ConfigureServices(services => { services.RegisterAppServices(); });
    try
    {
        var source = new CancellationTokenSource();
        var host = appBuilder.Build();
        using var scope = host.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ElasticSearchDataFiller>().ExecuteAsync(source.Token);
        await host.RunAsync(source.Token);
    }
    catch (Exception ex)
    {
    ex.WriteToLog("appBuilder..ConfigureHostConfiguration(cc=>cc.).Environment.EnvironmentName.ToLower()");
    }