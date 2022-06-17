using AuditService.ELK.FillTestData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args).Build();

var builder = new ConfigurationBuilder();

var fileProvider = new EnvironmentPathBuilder().GetParentRootPath();

builder.AddJsonFile("appsettings.json");
builder.AddJsonFile(fileProvider, "config/aus.api.appsettings.Development.json", true, true);
builder.AddJsonFile(fileProvider, "config/aus.api.logger.Development.json", true, true);
builder.AddEnvironmentVariables();

var configuration = builder.Build();

var client = new ElasticSearchConnector().CreateInstance(configuration);
var filler = new ElasticSearchDataFiller(client, configuration);

await filler.ExecuteAsync();
await host.RunAsync();