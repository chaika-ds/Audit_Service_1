using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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