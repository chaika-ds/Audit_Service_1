using AuditService.Setup;
using AuditService.Setup.Middleware;
using AuditService.Setup.Extensions;
using AuditService.Setup.ServiceConfigurations;
using AuditService.WebApi;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName.ToLower();

builder.Configuration.AddJsonFile("config/aus.api.appsettings.json", $"config/aus.api.env.{environmentName}.json", builder.Environment);
builder.Configuration.AddJsonFile($"config/aus.api.logger.{environmentName}.json", builder.Environment);
builder.Configuration.AddEnvironmentVariables();

builder.AddCustomerLogger(environmentName);

builder.Services.RegisterSettings();
builder.Services.AddControllersExtension();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddElasticSearch();
builder.Services.AdditionalConfigurations();
builder.Services.AddRedisCache();
builder.Services.AddSwagger();
builder.Services.RegisterServices();

var app = builder.Build();

if (!app.Environment.IsProduction())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseHealthChecks("/healthy");
app.MapControllers();
app.UseRouting();

app.UseMiddleware<AppMiddlewareException>();
app.UseMiddleware<RedisCacheMiddleware>();

app.Run();