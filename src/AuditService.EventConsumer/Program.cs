using AuditService.EventConsumer;
using AuditService.EventConsumerApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var environmentName = builder.Environment.EnvironmentName;

    builder.Configuration.AddJsonFile("appsettings.json");
    builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
    builder.Configuration.AddEnvironmentVariables();

    var additionalConfiguration = new AdditionalEnvironmentConfiguration();
    additionalConfiguration.AddJsonFile(builder, $"config/aus.api.appsettings.{environmentName}.json");

    builder.Services.AddApplicationServices(); 
    
    additionalConfiguration.AddCustomerLogger(builder, environmentName);

    var app = builder.Build();

    app.Run();
}
catch (Exception exception)
{
    Console.WriteLine(exception);
    Console.ReadKey();
    throw;
}
