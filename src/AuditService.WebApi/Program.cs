using AuditService.Setup.Middleware;
using AuditService.Utility.Logger;
using AuditService.Setup.Configurations;
using AuditService.Setup.Extensions;
using AuditService.WebApi;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", true);
builder.Configuration.AddJsonFile($"config/aus.api.appsettings.{environmentName}.json", builder.Environment);
builder.Configuration.AddJsonFile($"config/aus.api.logger.{environmentName}.json", builder.Environment);
builder.Configuration.AddEnvironmentVariables();

builder.AddCustomerLogger(environmentName);

builder.Services.AddControllers(options => { options.Filters.Add<LoggingActionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddElasticSearch();
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AdditionalConfigurations();

DiConfigure.Configure(builder.Services);

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
app.UseMiddleware<AuthenticateMiddleware>();
app.UseMiddleware<RedisCacheMiddleware>();

app.Run();