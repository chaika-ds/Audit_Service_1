using AuditService.EventConsumer;
using AuditService.Kafka;
using AuditService.Setup.ServiceConfigurations;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigs();
builder.AddLogger();

builder.Services.AddKafkaSettings();
builder.Services.KafkaServices();

var app = builder.Build();

app.Run();