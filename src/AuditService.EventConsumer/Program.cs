using AuditService.EventConsumerApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();

var app = builder.Build();

IWebHostEnvironment env = app.Environment;

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

app.Run();
