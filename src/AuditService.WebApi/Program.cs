using System.IO.Compression;
using AuditService.Utility.Logger;
using AuditService.WebApi;
using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;
using bgTeam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;


builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{environmentName}.json", true);

var additionalConfiguration = new AdditionalEnvironmentConfiguration();
additionalConfiguration
    .AddJsonFile(builder, $"config/aus.api.appsettings.{environmentName}.json");
additionalConfiguration.AddCustomerLogger(builder, environmentName);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(config =>
{
    config.Filters.Add<LoggingActionFilter>();
});

builder.Services.AddControllers();
//builder.Services.AddScoped<LoggingActionFilter>();
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
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
    options.ForwardedForHeaderName = "X-Original-Forwarded-For";
    options.RequireHeaderSymmetry = false;
    options.ForwardLimit = null;
});

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

app.UseMiddleware<AppMiddlewareException>();
app.UseMiddleware<AuthenticateMiddleware>();

app.Run();