using AuditService.Setup;
using AuditService.Setup.Middleware;
using AuditService.Setup.ServiceConfigurations;
using AuditService.Utility.Logger;
using AuditService.Utility.Logger.Filters;
using AuditService.WebApi;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.AddConfigs();
    builder.AddLogger();

    builder.Services.RegisterSettings();
    builder.Services.AddControllersWithFilters();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHealthChecks();
    builder.Services.AddElasticSearch();
    builder.Services.AdditionalConfigurations();
    builder.Services.AddRedisCache();
    builder.Services.AddSwagger();
    builder.Services.RegisterServices();
    builder.Services.AddMvcCore(options =>
    {
        options.Filters.Add<OperationCancelledExceptionFilter>();
    });

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
}
catch (Exception ex)
{
    ex.WriteToLog(builder.Environment.EnvironmentName.ToLower());
}