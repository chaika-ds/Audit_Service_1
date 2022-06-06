using System.IO.Compression;
using AuditService.WebApi;
using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

new AdditionalEnvironmentConfiguration()
    .AddJsonFile(builder, $"config/aus.api.appsettings.{builder.Environment.EnvironmentName}.json");

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