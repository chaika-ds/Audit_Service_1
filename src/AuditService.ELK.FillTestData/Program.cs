using AuditService.ELK.FillTestData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args).Build();

var config = host.Services.GetRequiredService<IConfiguration>();
var client = new ElasticSearchConnector().CreateInstance(host.Services);
var filler = new ElasticSearchDataFiller(client, config);

await filler.Execute();
await host.RunAsync();