using AuditService.Common.Extensions;
using AuditService.Setup;
using AuditService.Setup.Middleware;
using AuditService.Setup.ServiceConfigurations;
using AuditService.WebApi;
using KIT.NLog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureNLog();

try
{
    builder.AddConfigs();
    builder.Services.RegisterSettings();
    builder.Services.AddControllersWithFilters();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHealthChecks();
    builder.Services.AddElasticSearch();
    builder.Services.AdditionalConfigurations();
    builder.Services.AddRedisCache();
    builder.Services.AddSwagger();
    builder.Services.RegisterServices(builder.Environment.EnvironmentName);

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

    app.Run();
}
catch (Exception ex)
{
    NLog.LogManager.GetCurrentClassLogger().Error(ex, $"Stopped program because of exception: {ex.FullMessage()}");
}
finally
{
    NLog.LogManager.Shutdown();
}