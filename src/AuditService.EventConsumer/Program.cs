using System;
using AuditService.EventConsumer;
using AuditService.Kafka;
using AuditService.Setup.ServiceConfigurations;
using AuditService.Utility.Logger;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

try
{
    await builder.AddConfigsAsync();
    builder.AddLogger();

    builder.Services.AddKafkaSettings();
    builder.Services.KafkaServices();

    var app = builder.Build();

    app.Run();
}
catch (Exception e)
{
    e.WriteToLog(builder.Environment.EnvironmentName.ToLower());
}