using AuditService.WebApi;
using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);

new AdditionalEnvironmentConfiguration()
    .AddJsonFile(builder, $"config/aus.api.appsettings.{builder.Environment.EnvironmentName}.json");

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:ConnectionString"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"] ?? "RedisCache";
});

DiConfigure.Configure(builder.Services);
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