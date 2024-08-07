﻿using AuditService.ELK.FillTestData.Extensions;
using AuditService.ELK.FillTestData.Generators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var appBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, cfg) =>
        {            cfg.ConfigureAppConfiguration(hostingContext.HostingEnvironment);
        }).ConfigureServices(services => { services.RegisterAppServices(); });

var host = appBuilder.Build();
using var scope = host.Services.CreateScope();

await scope.ServiceProvider.GetRequiredService<AuditLogDataGenerator>().GenerateAsync();
await scope.ServiceProvider.GetRequiredService<BlockedPlayersLogDataGenerator>().GenerateAsync();
await scope.ServiceProvider.GetRequiredService<PlayerChangesLogDataLogDataGenerator>().GenerateAsync();
await scope.ServiceProvider.GetRequiredService<VisitLogGenerator>().GenerateAsync();
await scope.ServiceProvider.GetRequiredService<LossesLogGenerator>().GenerateAsync();

Environment.Exit(1);