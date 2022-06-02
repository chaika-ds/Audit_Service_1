using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AuditService.IntegrationTests.EventProducer.Settings
{
    internal class AppSettingsConsumer
    {
        public AppSettingsConsumer()
        {
            var application = new WebApplicationFactory<Program>();

            application.WithWebHostBuilder(builder =>
            {
                // Add services to the container.
                builder
                .ConfigureTestServices(services =>
                {
                    //services.AddTestServices();

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var director = scopedServices.GetRequiredService<IDirector>();

                    }
                })
                .UseEnvironment("Development")
                .ConfigureAppConfiguration(j => j.AddJsonFile("appsettings.json"));

                var app = builder.Build();

                app.Start();
            });

            //var client = application.CreateDefaultClient();
        }
    }
}
