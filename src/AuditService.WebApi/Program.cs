using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCache:ConnectionString"];
    options.InstanceName = builder.Configuration["RedisCache:InstanceName"];
});

DIConfigure.Configure(builder.Services);
ElasticConfiguration.Configure(builder.Services, builder.Configuration);

SwaggerConfiguration.Configure(builder.Services);
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
// Added here for docker support in production no need
SwaggerConfiguration.UseConfigure(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks("/_hc");
app.MapControllers();

app.Run();