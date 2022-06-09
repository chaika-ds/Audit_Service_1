using AuditService.WebApi.Configurations;
using AuditService.WebApi.Extensions;
using AuditService.WebApi.Middleware;
using AuditService.WebApiApp;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
var environmentName = builder.Environment.EnvironmentName;

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", true);
builder.Configuration.AddJsonFile($"config/aus.api.appsettings.{environmentName}.json", builder.Environment);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddElasticSearch();
builder.Services.AddSwagger(builder.Configuration);

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json; charset=utf-8" });
});

builder.Services.AdditionalConfigurations();

DiConfigure.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();