using AuditService.Setup;
using AuditService.Setup.Middleware;
using AuditService.Utility.Logger;
using AuditService.Setup.ServiceConfigurations;
using AuditService.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfigs();
builder.AddLogger();

builder.Services.RegisterSettings();
builder.Services.AddControllers(options => { options.Filters.Add<LoggingActionFilter>(); });
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