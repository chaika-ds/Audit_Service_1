using AuditService.EventConsumer;
using AuditService.EventConsumerApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

new AdditionalEnvironmentConfiguration()
    .AddJsonFile(builder, $"config/aus.api.appsettings.{builder.Environment.EnvironmentName}.json");

builder.Services.AddApplicationServices();

var app = builder.Build();

app.Run();