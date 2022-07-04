using System;
using AuditService.EventConsumer;
using AuditService.Kafka;
using AuditService.Setup.ServiceConfigurations;
using AuditService.Utility.Logger;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.AddConfigs();
    builder.AddLogger();

    builder.Services.AddKafkaSettings();
    builder.Services.AddKafkaServices();

    var app = builder.Build();

    app.Run();
}
catch (Exception e)
{
    e.WriteToLog(builder.Environment.EnvironmentName.ToLower());
}