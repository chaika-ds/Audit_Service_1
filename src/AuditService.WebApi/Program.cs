using AuditService.WebApi.Configurations;
using AuditService.WebApiApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

DIConfigure.Configure(builder.Services);

ElasticConfiguration.Configure(builder.Services);

SwaggerConfiguration.Configure(builder.Services);


builder.Services.AddHealthChecks();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    SwaggerConfiguration.UseConfigure(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks("/_hc");
app.MapControllers();

app.Run();