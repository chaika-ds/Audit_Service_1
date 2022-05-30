using AuditService.Common.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace AuditService.IntegrationTests
{
    public class AppSettings : IKafkaSettings, IDirectorSettings
    {
        private static readonly Lazy<AppSettings> lazy = new Lazy<AppSettings>(() => new AppSettings(), LazyThreadSafetyMode.PublicationOnly);
        public string GroupId { get; set; }

        public string Address { get; set; }

        public string Topic { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public Dictionary<string, string> Topics { get; set; }

        public AppSettings GetInstance(IConfiguration configuration)
        {
            GroupId = configuration["Kafka:GroupId"];
            Address = configuration["Kafka:Address"];
            Config = configuration.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            Topics = configuration.GetSection("KafkaTopics").GetChildren().ToDictionary(x => x.Key, v => v.Value);
            return lazy.Value;
        }

        internal AppSettings()
        {
            //var builder = WebApplicationFactory<Program>.WithWebHostBuilder(builder =>
            //{
            //        //        // Add services to the container.

            //        //        //builder.ConfigureServices(services =>
            //        //        //{
            //        //        //    services.SingleOrDefault(d=>d.); .AddServices();    
            //        //        //});

            //        //   // var app = builder.Build();

            //        //    //IWebHostEnvironment env = app.Environment;

            //        //    //builder.AddJsonFile("appsettings.json");
            //        //    //builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
            //        //    //builder.Configuration.AddEnvironmentVariables();

            //        //   // app.Run();

            //    });
        }
                
    }
}
