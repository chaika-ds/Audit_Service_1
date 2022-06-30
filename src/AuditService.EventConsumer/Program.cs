using AuditService.EventConsumer;
using AuditService.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

var additionalConfiguration = new AdditionalEnvironmentConfiguration();
additionalConfiguration.AddJsonFile(builder, $"config/aus.api.appsettings.{environmentName}.json");
additionalConfiguration.AddJsonFile(builder, $"config/aus.api.logger.{environmentName}.json");

builder.Services.AddKafkaSettings();
builder.Services.KafkaServices();

additionalConfiguration.AddCustomerLogger(builder, environmentName);

var app = builder.Build();

app.Run();