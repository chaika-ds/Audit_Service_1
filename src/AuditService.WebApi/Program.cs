using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
builder.Configuration.AddJsonFile($"config/aus.api.appsettings.{builder.Environment.EnvironmentName}.json", true);

//var configsPath = Directory.GetParent(builder.Environment.ContentRootPath)?.Parent?.Parent?.FullName;
//if (!string.IsNullOrEmpty(configsPath))
//{
//    builder.Configuration.AddJsonFile(new PhysicalFileProvider(Path.Combine(configsPath, "config")), $"aus.api.appsettings.{builder.Environment.EnvironmentName}.json", true, true);
//}
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:ConnectionString"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
});

DIConfigure.Configure(builder.Services);
ElasticConfiguration.Configure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditService.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks("/_hc");
app.MapControllers();

app.Run();